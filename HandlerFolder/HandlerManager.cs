using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TCPGameServer.Attributes;

namespace TCPGameServer.HandlerFolder
{
    public class HandlerManager
    {

        public List<SocketControllerAttribute> socketAttributeList = new List<SocketControllerAttribute>();
        public List<MethodInfo> methodInfos = new List<MethodInfo>();
        public List<Type> classes = new List<Type>();
        public List<SocketControllerAttribute> controllerlist = new List<SocketControllerAttribute>();
        public Dictionary<Opcodes, Action> controllerDict=new Dictionary<Opcodes, Action>();
        public HandlerManager()
        {
            TestGetControllers();
        }

        public async Task RunHandler(Opcodes opcode)
        {
            var a =controllerDict[Opcodes.GetProduct];
            byte[] bosarray=null;
            object instance = null;
            a.Invoke();
        }
        public void TestGetControllers()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                SocketControllerAttribute controller = type.GetCustomAttribute<SocketControllerAttribute>();
                if (type.IsSubclassOf(typeof(BaseHandler)) && controller !=null)
                {

                    controllerlist.Add(controller);
                    MethodInfo[] methods = type.GetMethods();
                    foreach(MethodInfo method in methods)
                    {
                        SocketActionAttribute socketActionAttribute = method.GetCustomAttribute<SocketActionAttribute>();
                        if (socketActionAttribute != null)
                        {

                            //controllerDict.Add(socketActionAttribute.opcode, method);


                            /*
                             (object instance, byte[] data) => {
                                ((ProductHandler)instance).Handle(data);
                             }
                             
                             */

                            
                            ParameterExpression prmInstance = Expression.Parameter(typeof(object), "instance");
                            ParameterExpression prmData = Expression.Parameter(typeof(byte[]), "data");

                            var expr = Expression.Call(Expression.Convert(prmInstance, type), method);

                            Expression<Action<object, byte[]>> lambda = Expression.Lambda<Action<object, byte[]>>(expr, new[]
                            {

                                prmInstance,
                                prmData,

                            });
                            //var aav = (type)Activator.CreateInstance(type, true);
                            //Expression<Action> fun = Expression.Lambda<Action>(Expression.Call((Expression)aav,method));

                           // Action hello=fun.Compile();
                            Action<object, byte[]> compiledMethod = lambda.Compile();
                            //controllerDict.Add(socketActionAttribute.opcode, hello);

                            Console.WriteLine(socketActionAttribute.opcode+"   "+method.Name);
                        }

                    }
                }
            }
        }

    }
}
