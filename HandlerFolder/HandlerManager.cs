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
        public Dictionary<Opcodes, MethodInfo> controllerDict =new Dictionary<Opcodes, MethodInfo>();
        public HandlerManager()
        {
            TestGetControllers();
        }

        public async Task RunHandler(Opcodes opcode, object[] objectArray)
        {
            MethodInfo method =controllerDict[opcode];
            Type methodtype = method.DeclaringType;
            object instance = Activator.CreateInstance(methodtype);

            ParameterInfo[] parameters = method.GetParameters();//if function has parameters it will get
 
            if (parameters.Length==0)
            {
                method.Invoke(instance, null);
            }
            else
            {
                method.Invoke(instance, objectArray);
            }

            await Console.Out.WriteLineAsync();

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

                            controllerDict.Add(socketActionAttribute.opcode, method);

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

                            //Console.WriteLine(socketActionAttribute.opcode+"   "+method.Name);
                        }

                    }
                }
            }
        }

    }
}
