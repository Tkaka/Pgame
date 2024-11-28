//Auto generated, do not modify it
//闄愬埗锛氬懡鍚嶄笉鑳戒互涓嬪垝绾垮紑澶达紝涓嶈兘澶稿懡鍚嶇┖闂寸户鎵�
//鍏煎闄愬埗1銆佸瓧娈靛彧鑳芥坊鍔狅紝娣诲姞鍚庝笉鑳藉垹闄わ紝瀛楁鍙兘娣诲姞鍒版渶鍚�
//鍏煎闄愬埗2銆佷笉鑳戒慨鏀瑰瓧娈电被鍨嬶紙濡備粠bool鏀逛负long锛�
//

using System;
using System.Collections.Generic;

namespace Message.BehaviourHook
{
    public enum _TypeEnum_
    {
        BaseHook = 1,
        HActorBehaviour = 2,
        HActorSpawner = 3,
        HSkillKeyFrame = 4,
        HMountPoint = 5,
        HSkillConfig = 6,
    }
						
    //
    public class BaseHook : BaseMessage
    {
		
        public List<int> hangPath{ get; protected set; } //

        //鏋勯�犲嚱鏁�
        public BaseHook() : base()
        {
			hangPath = new List<int>();
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			hangPath.Clear();
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								hangPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.BaseHook, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(1, _buffer_, ref _offset_);
				
				
				short _listCount_ = (short)hangPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(hangPath[_a_], _buffer_, ref _offset_);
				}
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class HActorBehaviour : BaseHook
    {
		
        public List<int> headBarPath{ get; protected set; } //血条路径
        public List<int> hitPosPath{ get; protected set; } //受击点
        public List<int> entranceShotPath{ get; protected set; } //出场动画镜头
		public float entranceNameTime; // 出场动画镜头显示boss名字延时

        //鏋勯�犲嚱鏁�
        public HActorBehaviour() : base()
        {
			headBarPath = new List<int>();
			hitPosPath = new List<int>();
			entranceShotPath = new List<int>();
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			headBarPath.Clear();
			hitPosPath.Clear();
			entranceShotPath.Clear();
			entranceNameTime = 0f;
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								headBarPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					if(_fieldNum_ > 1 && _fieldList_[1])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								hitPosPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					if(_fieldNum_ > 2 && _fieldList_[2])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								entranceShotPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					if(_fieldNum_ > 3 && _fieldList_[3])
					{
								entranceNameTime = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.HActorBehaviour, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(15, _buffer_, ref _offset_);
				
				
				short _listCount_ = (short)headBarPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(headBarPath[_a_], _buffer_, ref _offset_);
				}
				
				_listCount_ = (short)hitPosPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(hitPosPath[_a_], _buffer_, ref _offset_);
				}
				
				_listCount_ = (short)entranceShotPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(entranceShotPath[_a_], _buffer_, ref _offset_);
				}
						XBuffer.WriteFloat(entranceNameTime, _buffer_, ref _offset_);
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class HActorSpawner : BaseHook
    {
		
		public int actorType; // 实体类型

        //鏋勯�犲嚱鏁�
        public HActorSpawner() : base()
        {
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			actorType = 0;
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
								actorType = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.HActorSpawner, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(1, _buffer_, ref _offset_);
				
						XBuffer.WriteInt(actorType, _buffer_, ref _offset_);
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class HSkillKeyFrame : BaseMessage
    {
		
		public int keyFrame; // 
		public int type; // 
		public int ease; // 
		public int hurtCount; // 
		public int thunderPos; // 
		public float colMoveTime; // 
		public float colMoveDis; // 
		public int bulletModel; // 
		public int hurtState; // 
		public float FreezeTime; // 
		public bool back; // 

        //鏋勯�犲嚱鏁�
        public HSkillKeyFrame() : base()
        {
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			keyFrame = 0;
			type = 0;
			ease = 0;
			hurtCount = 0;
			thunderPos = 0;
			colMoveTime = 0f;
			colMoveDis = 0f;
			bulletModel = 0;
			hurtState = 0;
			FreezeTime = 0f;
			back = false;
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
								keyFrame = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 1 && _fieldList_[1])
					{
								type = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 2 && _fieldList_[2])
					{
								ease = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 3 && _fieldList_[3])
					{
								hurtCount = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 4 && _fieldList_[4])
					{
								thunderPos = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 5 && _fieldList_[5])
					{
								colMoveTime = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 6 && _fieldList_[6])
					{
								colMoveDis = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 7 && _fieldList_[7])
					{
								bulletModel = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 8 && _fieldList_[8])
					{
								hurtState = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 9 && _fieldList_[9])
					{
								FreezeTime = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 10 && _fieldList_[10])
					{
								back = XBuffer.ReadBool(_buffer_, ref _offset_);
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.HSkillKeyFrame, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(15, _buffer_, ref _offset_);
				
						XBuffer.WriteInt(keyFrame, _buffer_, ref _offset_);
						XBuffer.WriteInt(type, _buffer_, ref _offset_);
						XBuffer.WriteInt(ease, _buffer_, ref _offset_);
						XBuffer.WriteInt(hurtCount, _buffer_, ref _offset_);
						XBuffer.WriteInt(thunderPos, _buffer_, ref _offset_);
						XBuffer.WriteFloat(colMoveTime, _buffer_, ref _offset_);
						XBuffer.WriteFloat(colMoveDis, _buffer_, ref _offset_);
						XBuffer.WriteInt(bulletModel, _buffer_, ref _offset_);
						XBuffer.WriteInt(hurtState, _buffer_, ref _offset_);
						XBuffer.WriteFloat(FreezeTime, _buffer_, ref _offset_);
						XBuffer.WriteBool(back, _buffer_, ref _offset_);
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class HMountPoint : BaseHook
    {
		
		public int type; // 
        public List<int> transPath{ get; protected set; } //
		public bool isLocal; // 
		public bool onlyRotateY; // 

        //鏋勯�犲嚱鏁�
        public HMountPoint() : base()
        {
			transPath = new List<int>();
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			type = 0;
			transPath.Clear();
			isLocal = false;
			onlyRotateY = false;
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
								type = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 1 && _fieldList_[1])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								transPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					if(_fieldNum_ > 2 && _fieldList_[2])
					{
								isLocal = XBuffer.ReadBool(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 3 && _fieldList_[3])
					{
								onlyRotateY = XBuffer.ReadBool(_buffer_, ref _offset_);
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.HMountPoint, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(15, _buffer_, ref _offset_);
				
						XBuffer.WriteInt(type, _buffer_, ref _offset_);
				
				short _listCount_ = (short)transPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(transPath[_a_], _buffer_, ref _offset_);
				}
						XBuffer.WriteBool(isLocal, _buffer_, ref _offset_);
						XBuffer.WriteBool(onlyRotateY, _buffer_, ref _offset_);
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class HSkillConfig : BaseHook
    {
		
		public int aniName; // 
		public bool closeCombat; // 
		public int skillId; // 
		public int skillLevel; // 
		public int skillType; // 
		public int skillTemplate; // 
		public string headEffect; // 
		public string attackSound; // 
		public string hitSound; // 
		public string attackEffect; // 
		public string handEffect; // 
		public string hitEffect; // 
		public string bulletPrefab; // 
		public string bulletHitEffect; // 
		public float flySpeed; // 
		public int bulletDuration; // 
		public int bulletInterval; // 
		public int hitColor; // 
		public int colorElement; // 
		public int comboFrame; // 
		public float moveBack; // 
		public int skillTotalTime; // 
		public int standingPoint; // 
		public float standOffsetX; // 
		public float standOffsetY; // 
		public float hurtDelay; // 
		public bool hideOthers; // 
		public float onlyShowSelfTime; // 
		public float shakeCameraDur; // 
		public float shakeCameraStrength; // 
		public int shakeCameraVibrato; // 
		public float shakeCameraDistance; // 
        public List<int> closeShotPath{ get; protected set; } //
        public List<HSkillKeyFrame> skillFlowList{ get; protected set; } //
        public List<HSkillKeyFrame> keyframes{ get; protected set; } //
        public List<HSkillKeyFrame> otherKeyframes{ get; protected set; } //
        public List<HMountPoint> mountPoints{ get; protected set; } //

        //鏋勯�犲嚱鏁�
        public HSkillConfig() : base()
        {
			closeShotPath = new List<int>();
			skillFlowList = new List<HSkillKeyFrame>();
			keyframes = new List<HSkillKeyFrame>();
			otherKeyframes = new List<HSkillKeyFrame>();
			mountPoints = new List<HMountPoint>();
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			aniName = 0;
			closeCombat = false;
			skillId = 0;
			skillLevel = 0;
			skillType = 0;
			skillTemplate = 0;
			headEffect = null;
			attackSound = null;
			hitSound = null;
			attackEffect = null;
			handEffect = null;
			hitEffect = null;
			bulletPrefab = null;
			bulletHitEffect = null;
			flySpeed = 0f;
			bulletDuration = 0;
			bulletInterval = 0;
			hitColor = 0;
			colorElement = 0;
			comboFrame = 0;
			moveBack = 0f;
			skillTotalTime = 0;
			standingPoint = 0;
			standOffsetX = 0f;
			standOffsetY = 0f;
			hurtDelay = 0f;
			hideOthers = false;
			onlyShowSelfTime = 0f;
			shakeCameraDur = 0f;
			shakeCameraStrength = 0f;
			shakeCameraVibrato = 0;
			shakeCameraDistance = 0f;
			closeShotPath.Clear();
			skillFlowList.Clear();
			keyframes.Clear();
			otherKeyframes.Clear();
			mountPoints.Clear();
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				int _toReadLenOffset_ = _offset_;
				int _toReadLength_ = XBuffer.ReadInt(_buffer_, ref _offset_);
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
								aniName = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 1 && _fieldList_[1])
					{
								closeCombat = XBuffer.ReadBool(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 2 && _fieldList_[2])
					{
								skillId = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 3 && _fieldList_[3])
					{
								skillLevel = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 4 && _fieldList_[4])
					{
								skillType = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 5 && _fieldList_[5])
					{
								skillTemplate = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 6 && _fieldList_[6])
					{
								headEffect = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 7 && _fieldList_[7])
					{
								attackSound = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 8 && _fieldList_[8])
					{
								hitSound = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 9 && _fieldList_[9])
					{
								attackEffect = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 10 && _fieldList_[10])
					{
								handEffect = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 11 && _fieldList_[11])
					{
								hitEffect = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 12 && _fieldList_[12])
					{
								bulletPrefab = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 13 && _fieldList_[13])
					{
								bulletHitEffect = XBuffer.ReadString(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 14 && _fieldList_[14])
					{
								flySpeed = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 15 && _fieldList_[15])
					{
								bulletDuration = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 16 && _fieldList_[16])
					{
								bulletInterval = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 17 && _fieldList_[17])
					{
								hitColor = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 18 && _fieldList_[18])
					{
								colorElement = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 19 && _fieldList_[19])
					{
								comboFrame = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 20 && _fieldList_[20])
					{
								moveBack = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 21 && _fieldList_[21])
					{
								skillTotalTime = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 22 && _fieldList_[22])
					{
								standingPoint = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 23 && _fieldList_[23])
					{
								standOffsetX = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 24 && _fieldList_[24])
					{
								standOffsetY = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 25 && _fieldList_[25])
					{
								hurtDelay = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 26 && _fieldList_[26])
					{
								hideOthers = XBuffer.ReadBool(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 27 && _fieldList_[27])
					{
								onlyShowSelfTime = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 28 && _fieldList_[28])
					{
								shakeCameraDur = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 29 && _fieldList_[29])
					{
								shakeCameraStrength = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 30 && _fieldList_[30])
					{
								shakeCameraVibrato = XBuffer.ReadInt(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 31 && _fieldList_[31])
					{
								shakeCameraDistance = XBuffer.ReadFloat(_buffer_, ref _offset_);
					} else { break; }
					
					if(_fieldNum_ > 32 && _fieldList_[32])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								closeShotPath.Add(XBuffer.ReadInt(_buffer_, ref _offset_));
						}
					} else { break; }
					
					if(_fieldNum_ > 33 && _fieldList_[33])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								var _real_type_ = (_TypeEnum_)XBuffer.ReadByte(_buffer_, ref _offset_);
								HSkillKeyFrame _value_ = new HSkillKeyFrame();
								_offset_ = _value_.Read(_buffer_, _offset_);
								skillFlowList.Add(_value_);
						}
					} else { break; }
					
					if(_fieldNum_ > 34 && _fieldList_[34])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								var _real_type_ = (_TypeEnum_)XBuffer.ReadByte(_buffer_, ref _offset_);
								HSkillKeyFrame _value_ = new HSkillKeyFrame();
								_offset_ = _value_.Read(_buffer_, _offset_);
								keyframes.Add(_value_);
						}
					} else { break; }
					
					if(_fieldNum_ > 35 && _fieldList_[35])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								var _real_type_ = (_TypeEnum_)XBuffer.ReadByte(_buffer_, ref _offset_);
								HSkillKeyFrame _value_ = new HSkillKeyFrame();
								_offset_ = _value_.Read(_buffer_, _offset_);
								otherKeyframes.Add(_value_);
						}
					} else { break; }
					
					if(_fieldNum_ > 36 && _fieldList_[36])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								var _real_type_ = (_TypeEnum_)XBuffer.ReadByte(_buffer_, ref _offset_);
								HMountPoint _value_ = new HMountPoint();
								_offset_ = _value_.Read(_buffer_, _offset_);
								mountPoints.Add(_value_);
						}
					} else { break; }
					
					break;
				}
				
				//鍓旈櫎鏈煡鏁版嵁
				while(_offset_ - _toReadLenOffset_ < _toReadLength_)
					XBuffer.ReadByte(_buffer_, ref _offset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }

        public override int WriteWithType(byte[] _buffer_, int _offset_)
        {
            XBuffer.WriteByte((byte)_TypeEnum_.HSkillConfig, _buffer_, ref _offset_);
            _offset_ = Write(_buffer_, _offset_);
			return _offset_;
        }

        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				int _toWriteLenOffset_ = _offset_;
				XBuffer.WriteInt(0, _buffer_, ref _offset_);
				
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(255, _buffer_, ref _offset_);
				XBuffer.WriteByte(3, _buffer_, ref _offset_);
				
						XBuffer.WriteInt(aniName, _buffer_, ref _offset_);
						XBuffer.WriteBool(closeCombat, _buffer_, ref _offset_);
						XBuffer.WriteInt(skillId, _buffer_, ref _offset_);
						XBuffer.WriteInt(skillLevel, _buffer_, ref _offset_);
						XBuffer.WriteInt(skillType, _buffer_, ref _offset_);
						XBuffer.WriteInt(skillTemplate, _buffer_, ref _offset_);
						XBuffer.WriteString(headEffect, _buffer_, ref _offset_);
						XBuffer.WriteString(attackSound, _buffer_, ref _offset_);
						XBuffer.WriteString(hitSound, _buffer_, ref _offset_);
						XBuffer.WriteString(attackEffect, _buffer_, ref _offset_);
						XBuffer.WriteString(handEffect, _buffer_, ref _offset_);
						XBuffer.WriteString(hitEffect, _buffer_, ref _offset_);
						XBuffer.WriteString(bulletPrefab, _buffer_, ref _offset_);
						XBuffer.WriteString(bulletHitEffect, _buffer_, ref _offset_);
						XBuffer.WriteFloat(flySpeed, _buffer_, ref _offset_);
						XBuffer.WriteInt(bulletDuration, _buffer_, ref _offset_);
						XBuffer.WriteInt(bulletInterval, _buffer_, ref _offset_);
						XBuffer.WriteInt(hitColor, _buffer_, ref _offset_);
						XBuffer.WriteInt(colorElement, _buffer_, ref _offset_);
						XBuffer.WriteInt(comboFrame, _buffer_, ref _offset_);
						XBuffer.WriteFloat(moveBack, _buffer_, ref _offset_);
						XBuffer.WriteInt(skillTotalTime, _buffer_, ref _offset_);
						XBuffer.WriteInt(standingPoint, _buffer_, ref _offset_);
						XBuffer.WriteFloat(standOffsetX, _buffer_, ref _offset_);
						XBuffer.WriteFloat(standOffsetY, _buffer_, ref _offset_);
						XBuffer.WriteFloat(hurtDelay, _buffer_, ref _offset_);
						XBuffer.WriteBool(hideOthers, _buffer_, ref _offset_);
						XBuffer.WriteFloat(onlyShowSelfTime, _buffer_, ref _offset_);
						XBuffer.WriteFloat(shakeCameraDur, _buffer_, ref _offset_);
						XBuffer.WriteFloat(shakeCameraStrength, _buffer_, ref _offset_);
						XBuffer.WriteInt(shakeCameraVibrato, _buffer_, ref _offset_);
						XBuffer.WriteFloat(shakeCameraDistance, _buffer_, ref _offset_);
				
				short _listCount_ = (short)closeShotPath.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						XBuffer.WriteInt(closeShotPath[_a_], _buffer_, ref _offset_);
				}
				
				_listCount_ = (short)skillFlowList.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						if(skillFlowList[_a_] == null)
							UnityEngine.Debug.LogError("skillFlowList has nil item, idx == " + _a_);
						else
							_offset_ = skillFlowList[_a_].WriteWithType(_buffer_, _offset_);
				}
				
				_listCount_ = (short)keyframes.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						if(keyframes[_a_] == null)
							UnityEngine.Debug.LogError("keyframes has nil item, idx == " + _a_);
						else
							_offset_ = keyframes[_a_].WriteWithType(_buffer_, _offset_);
				}
				
				_listCount_ = (short)otherKeyframes.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						if(otherKeyframes[_a_] == null)
							UnityEngine.Debug.LogError("otherKeyframes has nil item, idx == " + _a_);
						else
							_offset_ = otherKeyframes[_a_].WriteWithType(_buffer_, _offset_);
				}
				
				_listCount_ = (short)mountPoints.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						if(mountPoints[_a_] == null)
							UnityEngine.Debug.LogError("mountPoints has nil item, idx == " + _a_);
						else
							_offset_ = mountPoints[_a_].WriteWithType(_buffer_, _offset_);
				}
				
				XBuffer.WriteInt(_offset_ - _toWriteLenOffset_, _buffer_, ref _toWriteLenOffset_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
    //
    public class Hook : BaseMessage
    {
        public override int GetMsgId() { return MsgId; }
        public const int MsgId = 0;
		
        public List<BaseHook> hookList{ get; protected set; } //

        //鏋勯�犲嚱鏁�
        public Hook() : base()
        {
			hookList = new List<BaseHook>();
        }
		
		//涓嶇紦瀛樺彲浠ヤ笉璋冪敤
		public override void Reset()
		{
			base.Reset();
			hookList.Clear();
		}
		
        //璇诲彇鏁版嵁
        public override int Read(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Read(_buffer_, _offset_);
			
				
				List<bool> _fieldList_ = new List<bool>();
				while(true)
				{
					var _fieldMark_ = XBuffer.ReadByte(_buffer_, ref _offset_);
					for(int i = 0; i < 7; ++i)
					{
						var _h_ = 1 << i;
						bool _mark_ = (_fieldMark_ & _h_) == _h_;
						if(_mark_) _fieldList_.Add(true);
						else break;
					}
					var _e_ = 1 << 7;
					if((_fieldMark_ & _e_) == 0)
						break;
				}
				
				int _fieldNum_ = _fieldList_.Count;
				
				while(true)
				{
					if(_fieldNum_ > 0 && _fieldList_[0])
					{
						short _count_ = XBuffer.ReadShort(_buffer_, ref _offset_);
						for(int _a_ = 0; _a_ < _count_; ++_a_)
						{
								var _real_type_ = (_TypeEnum_)XBuffer.ReadByte(_buffer_, ref _offset_);
								BaseHook _value_ = null;
								switch(_real_type_)
								{
									case _TypeEnum_.BaseHook : _value_ = new BaseHook(); break;
									case _TypeEnum_.HMountPoint : _value_ = new HMountPoint(); break;
									case _TypeEnum_.HActorBehaviour : _value_ = new HActorBehaviour(); break;
									case _TypeEnum_.HActorSpawner : _value_ = new HActorSpawner(); break;
									case _TypeEnum_.HSkillConfig : _value_ = new HSkillConfig(); break;
									default:break;
								}
								_offset_ = _value_.Read(_buffer_, _offset_);
								hookList.Add(_value_);
						}
					} else { break; }
					
					break;
				}
				
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }


        //鍐欏叆鏁版嵁
        public override int Write(byte[] _buffer_, int _offset_)
        {
            try
            {
                _offset_ = base.Write(_buffer_, _offset_);
				
				
				XBuffer.WriteByte(1, _buffer_, ref _offset_);
				
				
				short _listCount_ = (short)hookList.Count;
				XBuffer.WriteShort(_listCount_, _buffer_, ref _offset_);
				for (int _a_ = 0; _a_ < _listCount_; ++_a_)
                {
						if(hookList[_a_] == null)
							UnityEngine.Debug.LogError("hookList has nil item, idx == " + _a_);
						else
							_offset_ = hookList[_a_].WriteWithType(_buffer_, _offset_);
				}
				
            }
            catch(Exception ex)
            {
                throw ex;
            }
			return _offset_;
        }
    }
}