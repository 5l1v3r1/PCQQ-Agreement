﻿using QQ.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQ.Framework.Packets.Send.Login
{
    public class Send_0x0825 : SendPacket
    {
        /// <summary>
        /// 重定向标识
        /// </summary>
        bool redirect { get; set; } = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="User"></param>
        /// <param name="Key">数据包密钥</param>
        /// <param name="Redirect">是否是重定向包</param>
        public Send_0x0825(QQUser User, bool Redirect)
            : base(User)
        {
            if (Redirect)
            {
                Sequence = (char)0x3102;
            }
            else
            {
                Sequence = (char)0x3101;
            }
            redirect = Redirect;
            if (!Redirect)
            {
                _secretKey = user.QQ_PACKET_0825KEY;
            }
            else
            {
                _secretKey = user.QQ_PACKET_REDIRECTIONKEY;
            }
            Command = QQCommand.Login0x0825;
        }
        public override string GetPacketName()
        {
            return "登录包0x0825（Ping）";
        }

        protected override void PutHeader(ByteBuffer buf)
        {
            base.PutHeader(buf);
            buf.Put(user.QQ_PACKET_FIXVER);
            buf.Put(_secretKey);
        }
        /// <summary>
        /// 初始化包体
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void PutBody(ByteBuffer buf)
        {
            if (!redirect)
            {
                buf.Put(user.QQ_PACKET_0825DATA0);
                buf.Put(user.QQ_PACKET_0825DATA2);
                buf.PutLong(user.QQ);
                buf.Put(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x03, 0x09, 0x00, 0x08, 0x00, 0x01 });
                buf.Put(user.ServerIp);
                buf.Put(new byte[] { 0x00, 0x02, 0x00, 0x36, 0x00, 0x12, 0x00, 0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x14, 0x00, 0x1D, 0x01, 0x02, 0x00, 0x19 });
                buf.Put(user.QQ_PUBLIC_KEY);
            }
            else
            {
                buf.Put(user.QQ_PACKET_0825DATA0);
                buf.Put(user.QQ_PACKET_0825DATA2);
                buf.PutLong(user.QQ);
                buf.Put(new byte[] { 0x00, 0x01, 0x00, 0x00, 0x03, 0x09, 0x00, 0x0C, 0x00, 0x01 });
                buf.Put(user.ServerIp);
                buf.Put(new byte[] { 0x01, 0x6F, 0xA1, 0x58, 0x22, 0x01, 0x00, 0x36, 0x00, 0x12, 0x00, 0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x14, 0x00, 0x1D, 0x01, 0x03, 0x00, 0x19 });
                buf.Put(user.QQ_PUBLIC_KEY);
            }
        }
    }
}
