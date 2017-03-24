//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace APS.ClientUI
//{
//    /// <summary>
//    /// <para> 类描述：心跳监视服务 </para>
//    /// <para> 类说明：监视客户端与服务端之间的心跳连接 </para>
//    /// <para> 最后编辑人：徐金泽 </para>
//    /// <para> 最后编辑时间：2014年8月13日09:08:56 </para>
//    /// <para> 备注： </para>
//    /// </summary>
//    public class HeartBeatService

//        #region 单例模式

//        /// <summary>
//        /// <para> 方法描述：构造函数 </para>
//        /// <para> 方法说明：为实现单例模式创建的私有构造函数 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/8/2 15:02:49 </para>
//        /// <para> 编辑原因：编辑原因</para>
//        /// </summary>
//        private HeartBeatService()
//        {
//        }

//        /// <summary>
//        /// 获得对象实例 属性字段
//        /// <para>关联属性: Instance</para>
//        /// </summary>
//        private static HeartBeatService _instance;
//        /// <summary>
//        /// 获得对象实例 (只读)
//        /// </summary>
//        public static HeartBeatService Instance
//        {
//            get
//            {
//                lock (LockObj)
//                {
//                    if (_instance == null)
//                    {
//                        _instance = new HeartBeatService();
//                    }
//                    return _instance;
//                }
//            }
//        }
//        /// <summary>
//        /// 线程锁
//        /// </summary>
//        private static readonly object LockObj = new object();

//        #endregion

//        /// <summary>
//        /// 判断心跳服务是否已经打开
//        /// </summary>
//        private bool IsOpen = false;
//        /// <summary>
//        /// <para> 方法描述：开始服务 </para>
//        /// <para> 方法说明：开始建立于服务器的心跳连接 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/8/13 9:21:08 </para>
//        /// <para> 编辑原因：</para>
//        /// </summary>
//        public void Start()
//        {
//            if (!IsOpen)
//            {
//                IsOpen = true;
//                c1 = new ThreadEngine();
//                c2 = new ThreadEngine();
//                c1.Cycle = 1000;
//                c2.Cycle = 1000;
//                c1.CanInterruptTime = 1000 * 3600 * 12;
//                c2.CanInterruptTime = 1000 * 20;
//                c2.CanReStart = true;
//                c1.DoSeamThing = new Func<int>(SendHeartbeat);
//                c2.DoSeamThing = new Func<int>(Reconnect);
//                c1.Start();
//                //Thread SendPackageThread = new Thread(SendHeartbeat);
//                //SendPackageThread.IsBackground = true;
//                //SendPackageThread.Start();
//            }
//        }

//        private int SendHeartbeat()
//        {
//            try
//            {
//                HeartBeatResponse response = ClientService.SendHeartBeat(new HeartBeat()
//                {
//                    BeatData = null,
//                    BeatTime = DateTime.Now.Ticks,
//                    IP = AuthentificationService.Instance.CurrentUser.OSIP,
//                    MAC = AuthentificationService.Instance.CurrentUser.OSMAC
//                });
//                if (_receivePackage != null) _receivePackage.Invoke(response);
//            }
//            catch (Exception ex)
//            {
//                if (_sendPackageError != null) _sendPackageError.Invoke(ex);
//                c1.InterruptFlag = true;
//                c2.Start();
//            }
//            return 0;
//        }
//        /// <summary>
//        ///  待处理队列
//        /// </summary>
//        private Queue<List<RTD>> alarmDatas;
//        private int Reconnect()
//        {
//            short a;
//            try
//            {
//                alarmDatas = new Queue<List<RTD>>();
//                a = ClientService.Register(ClientType.EClient);
//                List<short> dd = new List<short>(20000);
//                for (short i = 0; i < 20000; i++)
//                {
//                    dd.Add(i);
//                }

//                int d = ClientService.Subscribe(dd);
//                ClientService.SendDatasByServerEvent += new ClientService.SendDatasByServerDelegate(ClientService_SendDatasByServerEvent);
//                //重新登录
//                UserSession user = AuthentificationService.Instance.CurrentUser;
//                UserSession newUser = RMTDT.ClientService.Invoke<UserSession>("", "ClientConnectionService", "ClientLogin", new object[] { user.UserName, user.Password, user.OSMAC, user.OSIP });
//                AuthentificationService.Instance.CurrentUser = newUser;
//            }
//            catch (Exception ex)
//            {
//                a = 0;
//            }
//            if (a == 1)
//            {
//                c2.Stop();
//                c1.InterruptFlag = false;
//            }
//            return 0;
//        }
//        /// <summary>
//        /// 处理从服务端接收的数据
//        /// </summary>
//        /// <param name="datas"></param>
//        private void ClientService_SendDatasByServerEvent(List<RTD> datas)
//        {
//            // 添加到待处理队列
//            alarmDatas.Enqueue(datas);
//        }

//        /// <summary>
//        /// <para> 方法描述：发送心跳方法 </para>
//        /// <para> 方法说明：定时发送心跳方法确保与服务器端通信传输正常 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/8/8 10:28:07 </para>
//        /// <para> 编辑原因：实现发送心跳包</para>
//        /// </summary>
//        /// <remarks>
//        ///  备注内容
//        /// </remarks>
//        /// <returns>返回值描述</returns>
//        //private void SendPackage()
//        //{
//        //    try
//        //    {
//        //        while (true)
//        //        {
//        //HeartBeatResponse response = ClientService.SendHeartBeat(new HeartBeat()
//        //{
//        //    BeatData = null,
//        //    BeatTime = DateTime.Now.Ticks,
//        //    IP = AuthentificationService.Instance.CurrentUser.OSIP,
//        //    MAC = AuthentificationService.Instance.CurrentUser.OSMAC
//        //});
//        //if(_receivePackage!=null) _receivePackage.Invoke(response);
//        //            Thread.Sleep(10000);
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if(_sendPackageError!=null)_sendPackageError.Invoke(ex);
//        //    }
//        //}

//        /// <summary>
//        /// 心跳错误事件 事件
//        /// </summary>
//        private event Action<Exception> _sendPackageError;
//        /// <summary>
//        /// 心跳错误事件 事件访问器
//        /// </summary>
//        public event Action<Exception> SendPackageErrorEvent
//        {
//            add
//            {
//                _sendPackageError += value;
//            }
//            remove
//            {
//                _sendPackageError -= value;
//            }
//        }

//        /// <summary>
//        /// 接收到服务器心跳包信息 事件
//        /// </summary>
//        private event Action<HeartBeatResponse> _receivePackage;
//        /// <summary>
//        /// 接收到服务器心跳包信息 事件访问器
//        /// </summary>
//        public event Action<HeartBeatResponse> ReceivePackageEvent
//        {
//            add
//            {
//                _receivePackage += value;
//            }
//            remove
//            {
//                _receivePackage -= value;
//            }
//        }

//    }
//}