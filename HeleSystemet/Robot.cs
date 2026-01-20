using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HeleSystemet;

public class Robot
{
    private readonly string _ipAddress;
    private readonly int _dashboardPort;
    private readonly int _urscriptPort;

    private readonly TcpClient _clientDashboard = new();
    private readonly TcpClient _clientUrscript = new();

    private Stream _streamDashboard = null!;
    private StreamReader _streamReaderDashboard = null!;
    private Stream _streamUrscript = null!;

    public Robot(string ipAddress = "172.20.254.202", int dashboardPort = 29999, int urscriptPort = 30002)
    {
        _ipAddress = ipAddress;
        _dashboardPort = dashboardPort;
        _urscriptPort = urscriptPort;
    }

    public bool Connected => _clientDashboard.Connected && _clientUrscript.Connected;

    public string RobotMode
    {
        get
        {
            SendDashboard("robotmode\n");
            return ReadLineDashboard();
        }
    }

    public void Connect()
    {
        _clientDashboard.Connect(_ipAddress, _dashboardPort);
        _streamDashboard = _clientDashboard.GetStream();
        _streamReaderDashboard = new StreamReader(_streamDashboard, Encoding.ASCII);

        // Consume Dashboard welcome message
        ReadLineDashboard();

        _clientUrscript.Connect(_ipAddress, _urscriptPort);
        _streamUrscript = _clientUrscript.GetStream();
    }

    public async Task PowerOnAsync()
    {
        SendDashboard("power on\n");
        ReadLineDashboard(); // consume reply (ofte "Powering on")

        // Vent indtil robotten IKKE længere er POWER_OFF
        for (int i = 0; i < 30; i++) // max 30 sek
        {
            var mode = RobotMode; // fx "Robotmode: POWER_ON" osv
            if (!mode.Contains("POWER_OFF"))
                return;

            await Task.Delay(1000);
        }

        throw new Exception("PowerOn timeout. Tjek Remote mode / fejl på teach pendant.");
    }

    public async Task BrakeReleaseAsync()
    {
        SendDashboard("brake release\n");
        ReadLineDashboard(); // consume reply

        // Vent indtil robotten er klar (ofte RUNNING)
        for (int i = 0; i < 30; i++)
        {
            var mode = RobotMode;
            if (mode.Contains("RUNNING"))
                return;

            await Task.Delay(1000);
        }

        // Hvis den aldrig rammer RUNNING er det stadig OK nogle gange,
        // så vi giver en mere informativ fejl:
        throw new Exception("BrakeRelease timeout. Tjek protective stop / sikkerhed / remote.");
    }

    public void Disconnect()
    {
        _clientDashboard.Close();
        _clientUrscript.Close();
    }

    public void SendDashboard(string command)
    {
        _streamDashboard.Write(Encoding.ASCII.GetBytes(command));
    }

    public void SendUrscript(string program)
    {
        _streamUrscript.Write(Encoding.ASCII.GetBytes(program));
    }

    public void SendUrscriptFile(string path)
    {
        var program = File.ReadAllText(path) + Environment.NewLine;
        SendUrscript(program);
    }

    public string ReadLineDashboard()
    {
        return _streamReaderDashboard.ReadLine() ?? "";
    }
}
