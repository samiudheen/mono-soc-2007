/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace NMS {
        /// <summary>
        /// The mode used to acknowledge messages after they are consumed
        /// </summary>
        public enum AcknowledgementMode
        {
                /**
                 * With this acknowledgment mode, the session automatically
                 * acknowledges a client's receipt of a message either when
                 * the session has successfully returned from a call to receive
                 * or when the message listener the session has called to
                 * process the message successfully returns.
                 */
                AutoAcknowledge,

                /**
                 * With this acknowledgment mode, the session automatically
                 * acknowledges a client's receipt of a message either when
                 * the session has successfully returned from a call to receive
                 * or when the message listener the session has called to
                 * process the message successfully returns.  Acknowlegements
                 * may be delayed in this mode to increase performance at
                 * the cost of the message being redelivered this client fails.
                 */
                DupsOkAcknowledge,

                /**
                 * With this acknowledgment mode, the client acknowledges a
                 * consumed message by calling the message's acknowledge method.
                 */
                ClientAcknowledge,

                /**
                 * Messages will be consumed when the transaction commits.
                 */
                Transactional
        }

        /// <summary>
        /// A delegate that can receive transport level exceptions.
        /// </summary>
        public delegate void ExceptionListener(Exception exception);


        /// <summary>
        /// Represents a connection with a message broker
        /// </summary>
        public interface IConnection : System.IDisposable, IStartable, IStoppable
        {

                /// <summary>
                /// Creates a new session to work on this connection
                /// </summary>
                ISession CreateSession();

                /// <summary>
                /// Creates a new session to work on this connection
                /// </summary>
                ISession CreateSession(AcknowledgementMode acknowledgementMode);


                /// <summary>
                /// The default acknowledgement mode
                /// </summary>
                AcknowledgementMode AcknowledgementMode { get; set; } 

                /// <summary>
                /// Sets the unique clienet ID for this connection before Start() or returns the
                /// unique client ID after the connection has started
                /// </summary>
                string ClientId { get; set; } 


                /// <summary>
                /// An asynchronous listener which can be notified if an error occurs
                /// </summary>
                event ExceptionListener ExceptionListener;

                /// <summary>
                /// Closes the connection.
                /// </summary>
                void Close();

        }
}
