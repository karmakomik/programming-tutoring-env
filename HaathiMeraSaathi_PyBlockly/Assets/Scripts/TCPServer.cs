using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

public class TCPServer : MonoBehaviour
{
    public static bool isReadingSocketStream;
    TcpListener server;
    // Start is called before the first frame update
    void Start()
    {
        isReadingSocketStream = true;
        InitializeConnection("127.0.0.1", 1337);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task InitializeConnection(string ip, int port)
    {
        //tcpClient = new TcpClient();
        server = new TcpListener(IPAddress.Parse(ip), port);
        server.Start();
        //await tcpClient.ConnectAsync(ip, port);
        var client = await server.AcceptTcpClientAsync().ConfigureAwait(false);
        var cw = new ClientWorking(client, true);
        Task.Run((Func<Task>)cw.DoSomethingWithClientAsync);
        Debug.Log("Connected to PyBlockly on {0}:{1}" + ip + port);
    }

    void OnDestroy()
    {
        isReadingSocketStream = false;
    }

    /*static async Task MainAsync()
    {
        Console.WriteLine("Starting...");
        var server = new TcpListener(IPAddress.Parse("0.0.0.0"), 66);
        server.Start();
        Debug.Log("Started.");
        while (true)
        {
            var client = await server.AcceptTcpClientAsync().ConfigureAwait(false);
            var cw = new ClientWorking(client, true);
            Task.Run((Func<Task>)cw.DoSomethingWithClientAsync);
        }
    }*/
}


class ClientWorking
{
    TcpClient _client;
    bool _ownsClient;

    public ClientWorking(TcpClient client, bool ownsClient)
    {
        _client = client;
        _ownsClient = ownsClient;
    }

    public async Task DoSomethingWithClientAsync()
    {
        try
        {
            using (var stream = _client.GetStream())
            {
                using (var sr = new StreamReader(stream))
                using (var sw = new StreamWriter(stream))
                {
                    await sw.WriteLineAsync("Connected to HMS").ConfigureAwait(false);
                    await sw.FlushAsync().ConfigureAwait(false);
                    
                    var data = default(string);
                    var code = default(string);
                    bool isBlockOfCode = false;
                    /*while (!((data = await sr.ReadLineAsync().ConfigureAwait(false)).Equals("exit", StringComparison.OrdinalIgnoreCase)))
                    {
                        await sw.WriteLineAsync("hello!").ConfigureAwait(false);
                        Debug.Log("Reply sent " + data);
                        await sw.FlushAsync().ConfigureAwait(false);
                    }*/
                    while (TCPServer.isReadingSocketStream)
                    {
                        data = await sr.ReadLineAsync().ConfigureAwait(false);
                        if (data.Equals("CODE_END"))
                        {
                            isBlockOfCode = false;
                            Debug.Log("Code sent : \n" + code);
                            code = "";
                        }
                        if (isBlockOfCode)
                        {
                            code += data + '\n';
                        }
                        if (data.Equals("CODE_BEGIN"))
                        {
                            isBlockOfCode = true;
                        }
                        
                        //Debug.Log("Reply sent " + data);
                    }
                }

            }
        }
        finally
        {
            if (_ownsClient && _client != null)
            {
                (_client as IDisposable).Dispose();
                _client = null;
            }
        }
    }
}


