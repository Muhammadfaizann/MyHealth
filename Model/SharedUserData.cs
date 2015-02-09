using System;
using Android.Content;
using Android.Preferences;
using Android.App;
using System.Linq;

namespace MyHealthAndroid
{
	public class SharedUserData<T>
	{
		public string key { get; private set;}
		private T value;
		private string _preferenceName;
		private T _defaultValue;

		public SharedUserData (string Key, string PreferenceName, T defaultValue)
		{
			key = Key;
			_preferenceName = PreferenceName;
			_defaultValue = defaultValue;
		}
	
//		public T GetValueForKey (Context Con) 
//		{
//			var shared = Con.GetSharedPreferences(_preferenceName, FileCreationMode.WorldReadable);
//			value = (T)shared.All.Where(x => x.Key == key).FirstOrDefault().Value;
//			if (value == null) SaveValueForKey(Con, _defaultValue);
//			return value;
//		}
//
//		public SharedUserData<T> SaveValueForKey (Context Con, T value)
//		{
//			var shared = Con.GetSharedPreferences(_preferenceName, FileCreationMode.WorldWriteable);
//			var edit = shared.Edit();
//			var refType = value.GetType();
//			if (refType == typeof(string))
//			{
//				edit.PutString(key,(String)value);
//				edit.Commit();
//				value = value;
//				return this;
//
//			}
//			if (refType == typeof(bool))
//			{
//				edit.PutBoolean(key, (bool)value);
//				edit.Commit();
//				value = value;
//				return this;
//			}
//			if (refType == typeof(int))
//			{
//				edit.PutInt(key, (int)value);
//				edit.Commit();
//				value = value;
//				return this;
//			}
//			if (refType == typeof(float))
//			{
//				edit.PutFloat(key, (float)value);
//				edit.Commit();
//				value = value;
//				return this;
//			}
//			if (refType == typeof(long))
//			{
//				edit.PutLong(key, (long)value);
//				edit.Commit();
//				value = value;
//				return this;
//			}
//			throw new InvalidOperationException("Type not supported, only use String, Bool, Int, Float, Long");
//			return null;
//		}
	}
}

