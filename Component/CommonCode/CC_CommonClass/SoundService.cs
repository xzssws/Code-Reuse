//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Media;

//namespace APS.ClientUI
//{
//    /// <summary>
//    /// <para> 类描述：报警音响服务 [单例模式] </para>
//    /// <para> 类说明：提供报警音响等服务 </para>
//    /// <para> 最后编辑人：徐金泽</para>
//    /// <para> 最后编辑时间：2014/8/2 15:02:49 </para>
//    /// <para> 备注：备注内容 </para>
//    /// </summary>
//    public sealed class SoundService : System.ComponentModel.INotifyPropertyChanged
//    {
//        #region 单例模式

//        /// <summary>
//        /// <para> 方法描述：构造函数 </para>
//        /// <para> 方法说明：为实现单例模式创建的私有构造函数 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/8/2 15:02:49 </para>
//        /// <para> 编辑原因：编辑原因</para>
//        /// </summary>
//        private SoundService()
//        {
//            Init();
//        }

//        /// <summary>
//        /// 获得对象实例 属性字段
//        /// <para>关联属性: Instance</para>
//        /// </summary>
//        private static SoundService _instance;

//        /// <summary>
//        /// 获得对象实例 (只读)
//        /// </summary>
//        public static SoundService Instance
//        {
//            get
//            {
//                lock (LockObj)
//                {
//                    if (_instance == null)
//                    {
//                        _instance = new SoundService();
//                    }
//                    return _instance;
//                }
//            }
//        }

//        /// <summary>
//        /// 线程锁
//        /// </summary>
//        private static readonly object LockObj = new object();

//        #endregion 单例模式

//        /// <summary>
//        /// 客户端声音文件路径
//        /// </summary>
//        private const string SoundDir = "AlarmSounds";

//        /// <summary>
//        /// 是否启用延迟消音 属性字段
//        /// <para>关联属性: CanMuteLazy</para>
//        /// </summary>
//        private bool? _canMuteLazy;
//        /// <summary>
//        /// 是否启用延迟消音
//        /// </summary>
//        public bool? CanMuteLazy
//        {
//            get
//            {
//                if (_canMuteLazy == null) _canMuteLazy = RMTDT.ClientService.Invoke<List<Entity.SystemConfigModel>>("","SystemConfigDao","Selects",null).FirstOrDefault().SC_SoundState;
//                return _canMuteLazy;
//            }
//            set
//            {
//                _canMuteLazy = value;
//                if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CanMuteLazy"));
//            }
//        }

//        /// <summary>
//        /// 静音状态 属性字段
//        /// <para>关联属性: IsMute</para>
//        /// </summary>
//        private bool _isMute;

//        /// <summary>
//        /// 静音状态 (只读)
//        /// </summary>
//        public bool IsMute
//        {
//            get { return _isMute; }
//            private set { _isMute = value; if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsMute")); }
//        }

//        /// <summary>
//        /// 报警音乐集合
//        /// </summary>
//        private Dictionary<string, APS.Entity.SoundsConfigModel> SoundDic;

//        /// <summary>
//        /// 声音选项 属性字段
//        /// <para>关联属性: Sounds</para>
//        /// </summary>
//        private Dictionary<string, string> _sounds;

//        /// <summary>
//        /// 声音选项
//        /// </summary>
//        private Dictionary<string, string> Sounds
//        {
//            get
//            {
//                if (_sounds == null)
//                {
//                    _sounds = new Dictionary<string, string>();
//                    for (int i = 1; i <= 10; i++)
//                    {
//                        _sounds.Add(string.Format("提示音{0}", i.ToString()), string.Format("{0}\\{1}\\AlarmSound\\sound{2}.wav", Environment.CurrentDirectory, SoundDir, i));
//                    }
//                };
//                return _sounds;
//            }
//            set { _sounds = value; }
//        }

//        /// <summary>
//        /// 媒体播放器
//        /// </summary>
//        private SoundPlayer Player = new SoundPlayer();

//        /// <summary>
//        /// 属性值变更通知事件
//        /// </summary>
//        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

//        /// <summary>
//        /// <para> 方法描述：报警音响数据加载服务 </para>
//        /// <para> 方法说明：加载报警音响的声音文件 以及相关数据 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/9/2 16:04:42 </para>
//        /// <para> 编辑原因：更新获取报警音响数据</para>
//        /// </summary>
//        /// <remarks>
//        ///
//        /// </remarks>
//        private void Init()
//        {
//            //获取所有声音资源
//            var result = RMTDT.ClientService.Invoke<IList<APS.Entity.SoundsConfigModel>>("", "SoundsConfigDao", "Selects", null);
//            SoundDic = result.ToDictionary(t => t.SC_SoundType);
//            //加载数据
//        }

//        /// <summary>
//        /// 正在播放的文件路径
//        /// </summary>
//        public Uri PlayerUri { get; set; }

//        /// <summary>
//        /// <para> 方法描述：播放 </para>
//        /// <para> 方法说明：根据报警级别播放对应的声音 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/9/2 16:05:03 </para>
//        /// <para> 编辑原因：实现声音播放</para>
//        /// </summary>
//        /// <param name="level">报警级别</param>
//        /// <remarks>
//        ///
//        /// </remarks>
//        public void Play(Entity.AlarmLevel level)
//        {
//            Player.Stop();
//            switch (level)
//            {
//                case APS.Entity.AlarmLevel.HighHighAlarm:
//                    Player.SoundLocation = Sounds[SoundDic["1级报警"].SC_SoundFile];
//                    break;

//                case APS.Entity.AlarmLevel.HighAlarm:
//                    Player.SoundLocation = Sounds[SoundDic["2级报警"].SC_SoundFile];
//                    break;

//                case APS.Entity.AlarmLevel.NoAlarm:
//                    Player.SoundLocation = Sounds[SoundDic["3级报警"].SC_SoundFile];
//                    break;

//                case APS.Entity.AlarmLevel.LowAlarm:
//                    Player.SoundLocation = Sounds[SoundDic["4级报警"].SC_SoundFile];
//                    break;
//            }

//            if (!IsMute)
//            {
//                Player.PlayLooping();
//            }
//        }

//        /// <summary>
//        /// <para> 方法描述：停止 </para>
//        /// <para> 方法说明：停止当前报警音响正在播放的声音</para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/9/2 16:05:16 </para>
//        /// <para> 编辑原因：实现停止播放</para>
//        /// </summary>
//        /// <remarks>
//        ///
//        /// </remarks>
//        public void Stop()
//        {
//            Player.Stop();
//        }
//        /// <summary>
//        /// 消音线程 属性字段
//        /// <para>关联属性: MuteThread</para>
//        /// </summary>
//        private System.Threading.Thread _muteThread;
//        /// <summary>
//        /// 消音线程
//        /// </summary>
//        public System.Threading.Thread MuteThread
//        {
//            get
//            {
//                if (_muteThread == null) _muteThread = new System.Threading.Thread(MuteThreadExecute);
//                return _muteThread;
//            }

//            set
//            {
//                _muteThread = value;
//            }
//        }

//        /// <summary>
//        /// <para> 方法描述：延迟消音线程方法 </para>
//        /// <para> 方法说明：延迟消音线程方法 </para>
//        /// <para> 最后编辑人：徐金泽</para>
//        /// <para> 最后编辑时间：2014年9月18日20:36:42 </para>
//        /// <para> 编辑原因：</para>
//        /// </summary>
//        public void MuteThreadExecute(object param)
//        {
//            Player.Stop();
//            int time = Convert.ToInt32(param);
//            System.Threading.Thread.Sleep(time);
//            Player.PlayLooping();
//            IsMute = false;
//        }

//        /// <summary>
//        /// <para> 方法描述：静音 </para>
//        /// <para> 方法说明：在一段时间消除声音报警 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/9/2 16:05:32 </para>
//        /// <para> 编辑原因：实现静音功能</para>
//        /// </summary>
//        /// <param name="time">静音时间 (毫秒)</param>
//        /// <returns>是否成功</returns>
//        public bool Mute(int time)
//        {
//            if (!IsMute)
//            {
//                IsMute = true;
//                MuteThread = new System.Threading.Thread(MuteThreadExecute);
//                MuteThread.Start(time);
//                return true;
//            }
//            else // 后一次的延迟消音覆盖前一次消音 #徐金泽 2014年9月18日14:32:03
//            {
//                IsMute = true;
//                MuteThread.Abort();
//                MuteThread = new System.Threading.Thread(MuteThreadExecute);
//                MuteThread.Start(time);
//                return false;
//            }
//        }

//        /// <summary>
//        /// <para> 方法描述：静音操作 </para>
//        /// <para> 方法说明：对当前声音组件进行静音操作 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014/10/20 16:35:26</para>
//        /// <para> 编辑原因：取消判断 </para>
//        /// </summary>
//        public void MuteLoop()
//        {
//            if (MuteThread.ThreadState == System.Threading.ThreadState.Running)
//            {
//                MuteThread.Abort();
//            }
//            Player.Stop();
//            IsMute = true;
//        }

//        /// <summary>
//        /// <para> 方法描述：取消静音操作 </para>
//        /// <para> 方法说明：对当前声音组件进行静音操作 </para>
//        /// <para> 最后编辑人：徐金泽 </para>
//        /// <para> 最后编辑时间：2014年9月18日20:38:30 </para>
//        /// <para> 编辑原因：</para>
//        /// </summary>
//        public void UnMuteLoop()
//        {
//            if (IsMute)
//            {
//                if (MuteThread.ThreadState == System.Threading.ThreadState.Running)
//                {
//                    MuteThread.Abort();
//                }
//                Player.PlayLooping();
//                IsMute = false;
//            }
//            else
//            {
//                Player.PlayLooping();
//            }

//        }
//    }
//}