#nullable enable
using System;
using CXE.CoreFx.Enums;

namespace CXE.CoreFx

	/// <summary>
	/// Defines the method of type creation, as well as the lifetime of the instances created.
	/// </summary>
	public struct Registration : IEquatable<Registration>
	{
		public Lifetime Lifetime
		{
			get; set;
		}
		public Type? Type
		{
			get; set;
		}
		

		public Registration(Type? type = null, Lifetime lifetime = Lifetime.Scoped)
		{
			if (instance != null && lifetime != Lifetime.Singleton)
			{
				throw new Exception("Instance value is only valid for Singleton!");
			}

			var reflectiveConstructorCallRequired = factory == null && instance == null;

			if (reflectiveConstructorCallRequired)
			{
				if (type == null)
				{
					throw new Exception($@"Either {nameof(type)} or {nameof(factory)} or {nameof(instance)} must not be null!");
				}

				if (type.IsInterface)
				{
					throw new Exception($@"Implementation Type ({type.FullName}) must not be an interface!");
				}
			}

			Type = type;
			Lifetime = lifetime;
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			return Equals((Registration) obj);
		}

		/// <summary>
		/// Determines whether the specified Registration is equal to the current Registration.
		/// </summary>
		/// <param name="other">The Registration to compare with the current Registration.</param>
		/// <returns>True if the specified Registration is equal to the current Registration; otherwise, false.</returns>
		public bool Equals(Registration other)
		{
			if (Type != other.Type
				|| Lifetime != other.Lifetime
				|| Instance != other.Instance)
			{
				return false;
			}

			return Factory?.Method == other.Factory?.Method;
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + Type?.GetHashCode() ?? 0;
				hash = hash * 23 + Lifetime.GetHashCode();

				if (Factory != null)
				{
					hash = hash * 23 + Factory.Method.GetHashCode();
				}
				else if (Instance != null)
				{
					hash = hash * 23 + Instance.GetHashCode();
				}

				return hash;
			}
		}

		/// <summary>
		/// Determines whether two Registration objects are equal.
		/// </summary>
		/// <param name="left">The first Registration to compare.</param>
		/// <param name="right">The second Registration to compare.</param>
		/// <returns>True if the two Registration objects are equal; otherwise, false.</returns>
		public static bool operator ==(Registration left, Registration right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Determines whether two Registration objects are not equal.
		/// </summary>
		/// <param name="left">The first Registration to compare.</param>
		/// <param name="right">The second Registration to compare.</param>
		/// <returns>True if the two Registration objects are not equal; otherwise, false.</returns>
		public static bool operator !=(Registration left, Registration right)
		{
			return !(left == right);
		}
	}
