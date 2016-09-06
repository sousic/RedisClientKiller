using RedisClientKiller.Redis;
using ServiceStack.Redis;
using ServiceStack.Text;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisClientKiller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread redisThread = null;

        delegate void mySetLogListInsert(string log, ListBox listbox);
        private void SetLogListInsert(string log, ListBox listbox)
        {
            
            int _logListMax = 1000; //500개 제한

            if (listbox.Items.Count >= _logListMax)
            {
                listbox.Items.RemoveAt(0);
            }
            listbox.Items.Add("[" + DateTime.Now + "] >>> " + log);

            listbox.SelectedIndex = listbox.Items.Count - 1;
        }

        private void SetLogListInsert(string msg)
        {
            
            this.Invoke(new mySetLogListInsert(SetLogListInsert), new object[] { msg, this.listBox1 });
        }

        private void redisThreadProc()
        {
            string host = RedisAppConst.ReidsCacheStageServerList[0] as string;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host+",allowAdmin=true"))
            {
                var server = redis.GetServer(host, 6379);
                var clientList = server.ClientList();

                foreach (var client in clientList)
                {
                    if (client.IdleSeconds > 1000)
                    {
                        SetLogListInsert(client.Address + "/" + client.IdleSeconds);
                        server.ClientKill(client.Address);
                        SetLogListInsert("클라이언크 킬");
                    }
                }
            }   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using(ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost,allowAdmin=true"))
            {     
                var server = redis.GetServer("localhost",6379);
                var clientList = server.ClientList();

                foreach (var client in clientList)
                {
                    if (client.IdleSeconds > 1000)
                    {
                        SetLogListInsert(client.Address + "/" + client.IdleSeconds);
                        server.ClientKill(client.Address);
                        SetLogListInsert("클라이언크 킬");
                    }
                }
            }            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            redisThread = new Thread(redisThreadProc);
            redisThread.IsBackground = true;
            redisThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = RedisAppConst.ReidsCacheLiveServerList[0] as string;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(host + ",allowAdmin=true"))
            {
                var server = redis.GetServer(host, 6379);
                var clientList = server.ClientList();

                foreach (var client in clientList)
                {
                    if (client.IdleSeconds > 1000)
                    {
                        SetLogListInsert(client.Address + "/" + client.IdleSeconds);
                        server.ClientKill(client.Address);
                        SetLogListInsert("클라이언크 킬");
                    }
                }
            }
        }

    }
}
