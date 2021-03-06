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
//using ActiveMQ;
using NMS;
using NUnit.Framework;
using System;

namespace NMS.Test
{
    [ TestFixture ]
    abstract public class NMSPropertyTest : NMSTestSupport
    {
        // standard NMS properties
        protected string expectedText = "Hey this works!";
        protected string correlationID = "abc";
        protected ITemporaryQueue replyTo;
        protected byte priority = 5;
        protected String type = "FooType";
        protected String groupID = "MyGroup";
        protected int groupSeq = 1;
        
        // custom properties
        string customText = "Cheese";
        bool custom1 = true;
        byte custom2 = 12;
        short custom3 = 0x1234;
        int custom4 = 0x12345678;
        long custom5 = 0x1234567812345678;
        char custom6 = 'J';
		float custom7 = 2.1F;
		double custom8 = 2.3;
        
        [SetUp]
        override public void SetUp()
        {
            base.SetUp();
        }
        
        [TearDown]
        override public void TearDown()
        {
            base.TearDown();
        }
        
        [ Test ]
        public override void SendAndSyncReceive()
        {
            base.SendAndSyncReceive();
        }
        
        protected override IMessage CreateMessage()
        {
            ITextMessage message = Session.CreateTextMessage(expectedText);
            replyTo = Session.CreateTemporaryQueue();
            
            // lets set the headers
            message.NMSCorrelationID = correlationID;
            message.NMSReplyTo = replyTo;
            message.NMSPersistent = persistent;
            message.NMSPriority = priority;
            message.NMSType = type;
            message.Properties["NMSXGroupID"] = groupID;
            message.Properties["NMSXGroupSeq"] = groupSeq;
            
            // lets set the custom headers
            message.Properties["customText"] = customText;
            message.Properties["custom1"] = custom1;
            message.Properties["custom2"] = custom2;
            message.Properties["custom3"] = custom3;
            message.Properties["custom4"] = custom4;
            message.Properties["custom5"] = custom5;
            message.Properties["custom6"] = custom6;
            message.Properties["custom7"] = custom7;
            message.Properties["custom8"] = custom8;
            
            return message;
        }
        
        protected override void AssertValidMessage(IMessage message)
        {
            Assert.IsTrue(message is ITextMessage, "Did not receive a ITextMessage!");
            
            Console.WriteLine("Received Message: " + message);
            
            ITextMessage textMessage = (ITextMessage) message;
            String text = textMessage.Text;
            Assert.AreEqual(expectedText, text, "the message text");
            
            // compare standard NMS headers
            Assert.AreEqual(correlationID, message.NMSCorrelationID, "NMSCorrelationID");
            Assert.AreEqual(persistent, message.NMSPersistent, "NMSPersistent");
            Assert.AreEqual(priority, message.NMSPriority, "NMSPriority");
            Assert.AreEqual(type, message.NMSType, "NMSType");
            Assert.AreEqual(groupID, message.Properties["NMSXGroupID"], "NMSXGroupID");
            Assert.AreEqual(groupSeq, message.Properties["NMSXGroupSeq"], "NMSXGroupSeq");
            
            // compare custom headers
            Assert.AreEqual(customText, message.Properties["customText"], "customText");
			
            AssertReplyToValid(message);
			
            AssertNonStringProperties(message);
            
            // lets now look at some standard NMS headers
            Console.WriteLine("NMSExpiration: " + message.NMSExpiration);
            Console.WriteLine("NMSMessageId: " + message.NMSMessageId);
            Console.WriteLine("NMSRedelivered: " + message.NMSRedelivered);
            Console.WriteLine("NMSTimestamp: " + message.NMSTimestamp);
            Console.WriteLine("NMSXDeliveryCount: " + message.Properties["NMSXDeliveryCount"]);
            Console.WriteLine("NMSXProducerTXID: " + message.Properties["NMSXProducerTXID"]);
        }

		protected virtual void AssertReplyToValid(IMessage message)
		{
			Assert.AreEqual(replyTo, message.NMSReplyTo, "NMSReplyTo");
		}

		protected virtual void AssertNonStringProperties(IMessage message)
		{
			Assert.AreEqual(custom1, message.Properties["custom1"], "custom1");
            Assert.AreEqual(custom2, message.Properties["custom2"], "custom2");
            Assert.AreEqual(custom3, message.Properties["custom3"], "custom3");
            Assert.AreEqual(custom4, message.Properties["custom4"], "custom4");
			
            Assert.AreEqual(custom5, message.Properties["custom5"], "custom5");
            Object value6 = message.Properties["custom6"];
            Object expected6 = custom6;
            Console.WriteLine("actual type is: " + value6.GetType() + " value: " + value6);
            Console.WriteLine("expected type is: " + expected6.GetType() + " value: " + expected6);
            Assert.AreEqual(custom6, value6, "custom6 which is of type: " + value6.GetType());
			
            Assert.AreEqual(custom6, message.Properties["custom6"], "custom6");
            Assert.AreEqual(custom7, message.Properties["custom7"], "custom7");
			Assert.AreEqual(custom8, message.Properties["custom8"], "custom8");
			
			// compare generic headers
            Assert.AreEqual(custom1, message.Properties.GetBool("custom1"), "custom1");
            Assert.AreEqual(custom2, message.Properties.GetByte("custom2"), "custom2");
            Assert.AreEqual(custom3, message.Properties.GetShort("custom3"), "custom3");
            Assert.AreEqual(custom4, message.Properties.GetInt("custom4"), "custom4");
            Assert.AreEqual(custom5, message.Properties.GetLong("custom5"), "custom5");
            Assert.AreEqual(custom6, message.Properties.GetChar("custom6"), "custom6");
            Assert.AreEqual(custom7, message.Properties.GetFloat("custom7"), "custom7");
			Assert.AreEqual(custom8, message.Properties.GetDouble("custom8"), "custom8");
		}
    }
}



