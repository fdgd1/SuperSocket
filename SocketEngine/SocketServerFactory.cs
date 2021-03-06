﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Text;
using SuperSocket.ProtoBase;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Provider;

namespace SuperSocket.SocketEngine
{
    /// <summary>
    /// Default socket server factory
    /// </summary>
    [Export(typeof(ISocketServerFactory))]
    [ProviderMetadata("SocketServerFactory")]

    public class SocketServerFactory : ISocketServerFactory
    {
        #region ISocketServerFactory Members

        /// <summary>
        /// Creates the socket server.
        /// </summary>
        /// <typeparam name="TPackageInfo">The type of the request info.</typeparam>
        /// <param name="appServer">The app server.</param>
        /// <param name="listeners">The listeners.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public ISocketServer CreateSocketServer<TPackageInfo>(IAppServer appServer, ListenerInfo[] listeners, IServerConfig config)
            where TPackageInfo : IPackageInfo
        {
            if (appServer == null)
                throw new ArgumentNullException("appServer");

            if (listeners == null)
                throw new ArgumentNullException("listeners");

            if (config == null)
                throw new ArgumentNullException("config");

            switch(config.Mode)
            {
                case(SocketMode.Tcp):
                    return new AsyncSocketServer(appServer, listeners);
                case(SocketMode.Udp):
                    return new UdpSocketServer<TPackageInfo>(appServer, listeners);
                default:
                    throw new NotSupportedException("Unsupported SocketMode:" + config.Mode);
            }
        }

        #endregion
    }
}
