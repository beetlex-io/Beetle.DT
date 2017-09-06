using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Beetle.DTCore
{
	public class MessageController
	{
		private Dictionary<Type, DelegateHandler> mActions = new Dictionary<Type, DelegateHandler>();

		public void Register<MSGTYPE>(Action<MSGTYPE> action)
		{
			Register(typeof(MSGTYPE), action);
		}

		public void Register<MSGTYPE, T>(Action<MSGTYPE, T> action)
		{
			Register(typeof(MSGTYPE), action);
		}

		public void Register<MSGTYPE, T, T1>(Action<MSGTYPE, T, T1> action)
		{
			Register(typeof(MSGTYPE), action);
		}

		public void Register<MSGTYPE, T, T1, T2>(Action<MSGTYPE, T, T1, T2> action)
		{
			Register(typeof(MSGTYPE), action);
		}

		public void Register<MSGTYPE, T, T1, T2, T3>(Action<MSGTYPE, T, T1, T2, T3> action)
		{
			Register(typeof(MSGTYPE), action);
		}

		private void Register(Type type, Delegate action)
		{
			DelegateHandler handler = new DTCore.MessageController.DelegateHandler();
			handler.Action = action;
			handler.Handler = new DTCore.MethodHandler(action.Method);
			mActions[type] = handler;
		}

		public bool Invoke(params object[] parameters)
		{
			DelegateHandler handler;
			if (mActions.TryGetValue(parameters[0].GetType(), out handler))
			{

				handler.Invokd(parameters);
				//action.DynamicInvoke(parameters);
				return true;
			}
			return false;
		}

		public class DelegateHandler
		{

			public Delegate Action { get; set; }

			public MethodHandler Handler { get; set; }

			public object Invokd(params object[] parameters)
			{
				return Handler.Execute(Action.Target, parameters);
			}
		}

	}

	public class FieldHandler
	{
		public FieldHandler(FieldInfo field)
		{
			mGetValue = ReflectionHandlerFactory.FieldGetHandler(field);
			mSetValue = ReflectionHandlerFactory.FieldSetHandler(field);
			Field = field;
		}
		private FieldInfo mField;
		public FieldInfo Field
		{
			get
			{
				return mField;
			}
			private set
			{
				mField = value;
			}
		}
		private GetValueHandler mGetValue;
		public GetValueHandler GetValue
		{
			get
			{
				return mGetValue;
			}

		}
		private SetValueHandler mSetValue;
		public SetValueHandler SetValue
		{
			get
			{
				return mSetValue;
			}

		}
	}
	public class PropertyHandler
	{
		public PropertyHandler(PropertyInfo property)
		{
			if (property.CanWrite)
				mSetValue = ReflectionHandlerFactory.PropertySetHandler(property);
			if (property.CanRead)
				mGetValue = ReflectionHandlerFactory.PropertyGetHandler(property);
			mProperty = property;
			IndexProperty = mProperty.GetGetMethod().GetParameters().Length > 0;
		}
		private bool mIndexProperty;
		public bool IndexProperty
		{
			get
			{
				return mIndexProperty;
			}
			set
			{
				mIndexProperty = value;
			}
		}
		private PropertyInfo mProperty;
		public PropertyInfo Property
		{
			get
			{
				return mProperty;
			}
			set
			{
				mProperty = value;
			}
		}
		private GetValueHandler mGetValue;
		public GetValueHandler Get
		{
			get
			{
				return mGetValue;
			}

		}
		private SetValueHandler mSetValue;
		public SetValueHandler Set
		{
			get
			{
				return mSetValue;
			}

		}
	}
	public class MethodHandler
	{
		public MethodHandler(MethodInfo method)
		{
			mExecute = ReflectionHandlerFactory.MethodHandler(method);
			mInfo = method;
		}
		private MethodInfo mInfo;
		public MethodInfo Info
		{
			get
			{
				return mInfo;
			}
		}
		private FastMethodHandler mExecute;
		public FastMethodHandler Execute
		{
			get
			{
				return mExecute;
			}
		}
	}
	public class InstanceHandler
	{
		public InstanceHandler(Type type)
		{
			mInstance = ReflectionHandlerFactory.InstanceHandler(type);
		}
		private ObjectInstanceHandler mInstance;
		public ObjectInstanceHandler Instance
		{
			get
			{
				return mInstance;
			}
		}
	}
	public delegate object GetValueHandler(object source);
	public delegate object ObjectInstanceHandler();
	public delegate void SetValueHandler(object source, object value);
	public delegate object FastMethodHandler(object target, object[] paramters);
	public class ReflectionHandlerFactory
	{


		#region field handler

		private static Dictionary<FieldInfo, GetValueHandler> mFieldGetHandlers = new Dictionary<FieldInfo, GetValueHandler>();
		private static Dictionary<FieldInfo, SetValueHandler> mFieldSetHandlers = new Dictionary<FieldInfo, SetValueHandler>();
		public static GetValueHandler FieldGetHandler(FieldInfo field)
		{
			GetValueHandler handler;
			if (mFieldGetHandlers.ContainsKey(field))
			{
				handler = mFieldGetHandlers[field];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mFieldGetHandlers.ContainsKey(field))
					{
						handler = mFieldGetHandlers[field];
					}
					else
					{
						handler = CreateFieldGetHandler(field);
						mFieldGetHandlers.Add(field, handler);
					}

				}
			}
			return handler;
		}
		private static GetValueHandler CreateFieldGetHandler(FieldInfo field)
		{
			DynamicMethod dm = new DynamicMethod("", typeof(object), new Type[] { typeof(object) }, field.DeclaringType);
			ILGenerator ilGenerator = dm.GetILGenerator();
			ilGenerator.Emit(OpCodes.Ldarg_0);
			ilGenerator.Emit(OpCodes.Ldfld, field);
			EmitBoxIfNeeded(ilGenerator, field.FieldType);
			ilGenerator.Emit(OpCodes.Ret);
			return (GetValueHandler)dm.CreateDelegate(typeof(GetValueHandler));
		}
		public static SetValueHandler FieldSetHandler(FieldInfo field)
		{
			SetValueHandler handler;
			if (mFieldSetHandlers.ContainsKey(field))
			{
				handler = mFieldSetHandlers[field];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mFieldSetHandlers.ContainsKey(field))
					{
						handler = mFieldSetHandlers[field];
					}
					else
					{
						handler = CreateFieldSetHandler(field);
						mFieldSetHandlers.Add(field, handler);
					}
				}
			}
			return handler;
		}
		private static SetValueHandler CreateFieldSetHandler(FieldInfo field)
		{
			DynamicMethod dm = new DynamicMethod("", null, new Type[] { typeof(object), typeof(object) }, field.DeclaringType);
			ILGenerator ilGenerator = dm.GetILGenerator();
			ilGenerator.Emit(OpCodes.Ldarg_0);
			ilGenerator.Emit(OpCodes.Ldarg_1);
			EmitCastToReference(ilGenerator, field.FieldType);
			ilGenerator.Emit(OpCodes.Stfld, field);
			ilGenerator.Emit(OpCodes.Ret);
			return (SetValueHandler)dm.CreateDelegate(typeof(SetValueHandler));
		}

		#endregion

		#region Property Handler

		private static Dictionary<PropertyInfo, GetValueHandler> mPropertyGetHandlers = new Dictionary<PropertyInfo, GetValueHandler>();
		private static Dictionary<PropertyInfo, SetValueHandler> mPropertySetHandlers = new Dictionary<PropertyInfo, SetValueHandler>();
		public static SetValueHandler PropertySetHandler(PropertyInfo property)
		{
			SetValueHandler handler;
			if (mPropertySetHandlers.ContainsKey(property))
			{
				handler = mPropertySetHandlers[property];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mPropertySetHandlers.ContainsKey(property))
					{
						handler = mPropertySetHandlers[property];
					}
					else
					{
						handler = CreatePropertySetHandler(property);
						mPropertySetHandlers.Add(property, handler);
					}
				}
			}
			return handler;
		}
		private static SetValueHandler CreatePropertySetHandler(PropertyInfo property)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, null, new Type[] { typeof(object), typeof(object) }, property.DeclaringType.Module);

			ILGenerator ilGenerator = dynamicMethod.GetILGenerator();


			ilGenerator.Emit(OpCodes.Ldarg_0);


			ilGenerator.Emit(OpCodes.Ldarg_1);


			EmitCastToReference(ilGenerator, property.PropertyType);


			ilGenerator.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);


			ilGenerator.Emit(OpCodes.Ret);


			SetValueHandler setter = (SetValueHandler)dynamicMethod.CreateDelegate(typeof(SetValueHandler));

			return setter;
		}
		public static GetValueHandler PropertyGetHandler(PropertyInfo property)
		{
			GetValueHandler handler;
			if (mPropertyGetHandlers.ContainsKey(property))
			{
				handler = mPropertyGetHandlers[property];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mPropertyGetHandlers.ContainsKey(property))
					{
						handler = mPropertyGetHandlers[property];
					}
					else
					{
						handler = CreatePropertyGetHandler(property);
						mPropertyGetHandlers.Add(property, handler);
					}
				}
			}
			return handler;
		}
		private static GetValueHandler CreatePropertyGetHandler(PropertyInfo property)
		{

			DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object) }, property.DeclaringType.Module);

			ILGenerator ilGenerator = dynamicMethod.GetILGenerator();


			ilGenerator.Emit(OpCodes.Ldarg_0);


			ilGenerator.EmitCall(OpCodes.Callvirt, property.GetGetMethod(), null);


			EmitBoxIfNeeded(ilGenerator, property.PropertyType);


			ilGenerator.Emit(OpCodes.Ret);


			GetValueHandler getter = (GetValueHandler)dynamicMethod.CreateDelegate(typeof(GetValueHandler));

			return getter;
		}
		#endregion

		#region Method Handler

		private static Dictionary<MethodInfo, FastMethodHandler> mMethodHandlers = new Dictionary<MethodInfo, FastMethodHandler>();
		public static FastMethodHandler MethodHandler(MethodInfo method)
		{
			FastMethodHandler handler = null;
			if (mMethodHandlers.ContainsKey(method))
			{
				handler = mMethodHandlers[method];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mMethodHandlers.ContainsKey(method))
					{
						handler = mMethodHandlers[method];
					}
					else
					{
						handler = CreateMethodHandler(method);
						mMethodHandlers.Add(method, handler);
					}
				}
			}
			return handler;
		}
		private static FastMethodHandler CreateMethodHandler(MethodInfo methodInfo)
		{
			DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);
			ILGenerator il = dynamicMethod.GetILGenerator();
			ParameterInfo[] ps = methodInfo.GetParameters();
			Type[] paramTypes = new Type[ps.Length];
			for (int i = 0; i < paramTypes.Length; i++)
			{
				if (ps[i].ParameterType.IsByRef)
					paramTypes[i] = ps[i].ParameterType.GetElementType();
				else
					paramTypes[i] = ps[i].ParameterType;
			}
			LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

			for (int i = 0; i < paramTypes.Length; i++)
			{
				locals[i] = il.DeclareLocal(paramTypes[i], true);
			}
			for (int i = 0; i < paramTypes.Length; i++)
			{
				il.Emit(OpCodes.Ldarg_1);
				EmitFastInt(il, i);
				il.Emit(OpCodes.Ldelem_Ref);
				EmitCastToReference(il, paramTypes[i]);
				il.Emit(OpCodes.Stloc, locals[i]);
			}
			if (!methodInfo.IsStatic)
			{
				il.Emit(OpCodes.Ldarg_0);
			}
			for (int i = 0; i < paramTypes.Length; i++)
			{
				if (ps[i].ParameterType.IsByRef)
					il.Emit(OpCodes.Ldloca_S, locals[i]);
				else
					il.Emit(OpCodes.Ldloc, locals[i]);
			}
			if (methodInfo.IsStatic)
				il.EmitCall(OpCodes.Call, methodInfo, null);
			else
				il.EmitCall(OpCodes.Callvirt, methodInfo, null);
			if (methodInfo.ReturnType == typeof(void))
				il.Emit(OpCodes.Ldnull);
			else
				EmitBoxIfNeeded(il, methodInfo.ReturnType);

			for (int i = 0; i < paramTypes.Length; i++)
			{
				if (ps[i].ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldarg_1);
					EmitFastInt(il, i);
					il.Emit(OpCodes.Ldloc, locals[i]);
					if (locals[i].LocalType.IsValueType)
						il.Emit(OpCodes.Box, locals[i].LocalType);
					il.Emit(OpCodes.Stelem_Ref);
				}
			}

			il.Emit(OpCodes.Ret);
			FastMethodHandler invoder = (FastMethodHandler)dynamicMethod.CreateDelegate(typeof(FastMethodHandler));
			return invoder;
		}
		#endregion

		#region Instance Handler

		private static Dictionary<Type, ObjectInstanceHandler> mInstanceHandlers = new Dictionary<Type, ObjectInstanceHandler>();
		public static ObjectInstanceHandler InstanceHandler(Type type)
		{
			ObjectInstanceHandler handler;
			if (mInstanceHandlers.ContainsKey(type))
			{
				handler = mInstanceHandlers[type];
			}
			else
			{
				lock (typeof(ReflectionHandlerFactory))
				{
					if (mInstanceHandlers.ContainsKey(type))
					{
						handler = mInstanceHandlers[type];
					}
					else
					{
						handler = CreateInstanceHandler(type);
						mInstanceHandlers.Add(type, handler);
					}
				}
			}
			return handler;
		}
		private static ObjectInstanceHandler CreateInstanceHandler(Type type)
		{
			DynamicMethod method = new DynamicMethod(string.Empty, type, null, type.Module);
			ILGenerator il = method.GetILGenerator();
			il.DeclareLocal(type, true);
			il.Emit(OpCodes.Newobj, type.GetConstructor(new Type[0]));
			il.Emit(OpCodes.Stloc_0);
			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ret);
			ObjectInstanceHandler creater = (ObjectInstanceHandler)method.CreateDelegate(typeof(ObjectInstanceHandler));
			return creater;

		}
		#endregion


		private static void EmitCastToReference(ILGenerator il, System.Type type)
		{
			if (type.IsValueType)
			{
				il.Emit(OpCodes.Unbox_Any, type);
			}
			else
			{
				il.Emit(OpCodes.Castclass, type);
			}
		}
		private static void EmitBoxIfNeeded(ILGenerator il, System.Type type)
		{
			if (type.IsValueType)
			{
				il.Emit(OpCodes.Box, type);
			}
		}
		private static void EmitFastInt(ILGenerator il, int value)
		{
			switch (value)
			{
				case -1:
					il.Emit(OpCodes.Ldc_I4_M1);
					return;
				case 0:
					il.Emit(OpCodes.Ldc_I4_0);
					return;
				case 1:
					il.Emit(OpCodes.Ldc_I4_1);
					return;
				case 2:
					il.Emit(OpCodes.Ldc_I4_2);
					return;
				case 3:
					il.Emit(OpCodes.Ldc_I4_3);
					return;
				case 4:
					il.Emit(OpCodes.Ldc_I4_4);
					return;
				case 5:
					il.Emit(OpCodes.Ldc_I4_5);
					return;
				case 6:
					il.Emit(OpCodes.Ldc_I4_6);
					return;
				case 7:
					il.Emit(OpCodes.Ldc_I4_7);
					return;
				case 8:
					il.Emit(OpCodes.Ldc_I4_8);
					return;
			}

			if (value > -129 && value < 128)
			{
				il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
			}
			else
			{
				il.Emit(OpCodes.Ldc_I4, value);
			}
		}
	}
}
