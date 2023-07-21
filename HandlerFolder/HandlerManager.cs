using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Attributes;
using TCPGameServer.Dto;
using TCPGameServer.Services;

namespace TCPGameServer.HandlerFolder
{
    public class HandlerManager: IHandler
    {
        #region Deneme listeleri
        //public List<SocketControllerAttribute> socketAttributeList = new List<SocketControllerAttribute>();
        //public List<MethodInfo> methodInfos = new List<MethodInfo>();
        //public List<Type> classes = new List<Type>();
        //public List<SocketControllerAttribute> controllerlist = new List<SocketControllerAttribute>();
        #endregion


        public Dictionary<Opcodes, MethodInfo> controllerMethodDict = new Dictionary<Opcodes, MethodInfo>();
        public Dictionary<Type, object> controllerInstanceDict = new Dictionary<Type, object>();


        IClient _client;
        public HandlerManager()
        {
            TestGetControllers();
        }
        public async Task<string> PacketReceived(string jsonData)
        {
            Packet mainPacket = JsonConvert.DeserializeObject<Packet>(jsonData);
            //RegisterPacket  regpack= JsonConvert.DeserializeObject<RegisterPacket>(jsonData);
            object[] dataArray = new object[] { jsonData };
            string data=(string) await RunHandler((Opcodes)mainPacket.opcode, dataArray);
           // _client.SendDataFromJson(data);
            return data;
        }

        public async Task<string> RunHandler(Opcodes opcode, object[] objectArray)
        {
            MethodInfo method = controllerMethodDict[opcode];
            Type methodtype = method.DeclaringType;
            object instance = controllerInstanceDict[methodtype];

            ParameterInfo[] parameters = method.GetParameters();//if function has parameters it will get
            string jsondata;

            if (parameters.Length == 0)
            {
                jsondata=(string)method.Invoke(instance, null);
            }
            else
            {
                jsondata = (string)method.Invoke(instance, objectArray);
            }
            await Console.Out.WriteLineAsync();
            return jsondata;

        }
        public void TestGetControllers()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                SocketControllerAttribute controller = type.GetCustomAttribute<SocketControllerAttribute>();
                if (type.IsSubclassOf(typeof(BaseHandler)) && controller != null)
                {

                    //controllerlist.Add(controller);
                    MethodInfo[] methods = type.GetMethods();
                    foreach (MethodInfo method in methods)
                    {
                        SocketActionAttribute socketActionAttribute = method.GetCustomAttribute<SocketActionAttribute>();
                        if (socketActionAttribute != null)
                        {
                            controllerMethodDict.Add(socketActionAttribute.opcode, method);

                            //Creating instance of methods and adding to dictionary
                            Type methodtype = method.DeclaringType;
                            if (!controllerInstanceDict.ContainsKey(methodtype))
                            {
                                object instance = Activator.CreateInstance(methodtype);
                                controllerInstanceDict.Add(methodtype, instance);
                            }

                            #region Furkan

                            /*
                             (object instance, byte[] data) => {
                                ((ProductHandler)instance).Handle(data);
                             }
                             
                             

                            //Type typename = type;
                            ParameterExpression prmInstance = Expression.Parameter(typeof(object), "instance");
                            ParameterExpression prmData = Expression.Parameter(typeof(byte[]), "data");

                            var expr = Expression.Call(Expression.Convert(prmInstance, type), method);

                            Expression<Action<object, byte[]>> lambda = Expression.Lambda<Action<object, byte[]>>(expr, new[]
                            {

                                prmInstance,
                                prmData,

                            });
                            Action<object, byte[]> compiledMethod = lambda.Compile();
                            //controllerDict.Add(socketActionAttribute.opcode, compiledMethod);
                            */
                            #endregion
                            #region Alperen
                            //var aav = Activator.CreateInstance(type);


                            // Expression<Action> fun = Expression.Lambda<Action>(Expression.Call((Expression)aav,method));
                            //Action hello=fun.Compile();
                            #endregion
                        }

                    }
                }
            }
        }

    }
}
