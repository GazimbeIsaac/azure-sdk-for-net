﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure {
    /// <summary>
    /// Used to store primitive values without boxing, and other instances.
    /// </summary>
    public readonly partial struct Value
    {
        private readonly Union _union;
        private readonly object? _object;

        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="value"></param>
        public Value(object? value)
        {
            _object = value;
            _union = default;
        }

        /// <summary>
        /// Type of the instance stored in this value.
        /// </summary>
        public readonly Type? Type
        {
            get
            {
                Type? type;
                if (_object is null)
                {
                    type = null;
                }
                else if (_object is TypeFlag typeFlag)
                {
                    type = typeFlag.Type;
                }
                else
                {
                    type = _object.GetType();

                    if (_union.UInt64 != 0 && type.IsArray)
                    {
                        // We have an ArraySegment
                        if (type == typeof(byte[]))
                        {
                            type = typeof(ArraySegment<byte>);
                        }
                        else if (type == typeof(char[]))
                        {
                            type = typeof(ArraySegment<char>);
                        }
                        else
                        {
                            ThrowInvalidOperation();
                        }
                    }
                }

                return type;
            }
        }

        [DoesNotReturn]
        private static void ThrowInvalidCast() => throw new InvalidCastException();

        [DoesNotReturn]
        private static void ThrowArgumentNull(string paramName) => throw new ArgumentNullException(paramName);

        [DoesNotReturn]
        private static void ThrowInvalidOperation() => throw new InvalidOperationException();

        #region Byte
        /// <summary>
        /// Stores byte in this value.
        /// </summary>
        /// <param name="value"></param>
        public Value(byte value)
        {
            this = default;
            _object = TypeFlags.Byte;
            _union.Byte = value;
        }

        /// <summary>
        /// Stores nullable byte in this value.
        /// </summary>
        /// <param name="value"></param>
        public Value(byte? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Byte;
                _union.Byte = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// Casts byte to value.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(byte value) => new(value);
        /// <summary>
        /// Casts value to byte, if possible.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator byte(in Value value) => value.As<byte>();
        /// <summary>
        /// Casts nullable byte to value.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(byte? value) => new(value);
        /// <summary>
        /// Casts value to nullable byte, if possible.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator byte?(in Value value) => value.As<byte?>();
        #endregion

        #region SByte
        /// <summary>
        /// Stores sbyte in this value.
        /// </summary>
        /// <param name="value"></param>
        public Value(sbyte value)
        {
            this = default;
            _object = TypeFlags.SByte;
            _union.SByte = value;
        }

        /// <summary>
        /// Stores nullable sbyte in this value.
        /// </summary>
        /// <param name="value"></param>
        public Value(sbyte? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.SByte;
                _union.SByte = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// Casts sbyte to value.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(sbyte value) => new(value);
        /// <summary>
        /// Casts value to sbyte, if possible.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator sbyte(in Value value) => value.As<sbyte>();
        /// <summary>
        /// Casts nullable sbyte to value.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(sbyte? value) => new(value);
        /// <summary>
        /// Casts value to nullable sbyte, if possible.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator sbyte?(in Value value) => value.As<sbyte?>();
        #endregion

        #region Boolean
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(bool value)
        {
            this = default;
            _object = TypeFlags.Boolean;
            _union.Boolean = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(bool? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Boolean;
                _union.Boolean = value.Value;
            }
            else
            {
                _object = null;
            }
        }
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(bool value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator bool(in Value value) => value.As<bool>();
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(bool? value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator bool?(in Value value) => value.As<bool?>();
        #endregion

        #region Char
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(char value)
        {
            this = default;
            _object = TypeFlags.Char;
            _union.Char = value;
        }
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(char? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Char;
                _union.Char = value.Value;
            }
            else
            {
                _object = null;
            }
        }
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(char value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator char(in Value value) => value.As<char>();
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(char? value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator char?(in Value value) => value.As<char?>();
        #endregion

        #region Int16
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(short value)
        {
            this = default;
            _object = TypeFlags.Int16;
            _union.Int16 = value;
        }
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(short? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int16;
                _union.Int16 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(short value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator short(in Value value) => value.As<short>();
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(short? value) => new(value);
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator short?(in Value value) => value.As<short?>();
        #endregion

        #region Int32
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(int value)
        {
            this = default;
            _object = TypeFlags.Int32;
            _union.Int32 = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(int? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int32;
                _union.Int32 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(int value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator int(in Value value) => value.As<int>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(int? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator int?(in Value value) => value.As<int?>();
        #endregion

        #region Int64
        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(long value)
        {
            this = default;
            _object = TypeFlags.Int64;
            _union.Int64 = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(long? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int64;
                _union.Int64 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(long value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator long(in Value value) => value.As<long>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(long? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator long?(in Value value) => value.As<long?>();
        #endregion

        #region UInt16

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(ushort value)
        {
            this = default;
            _object = TypeFlags.UInt16;
            _union.UInt16 = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(ushort? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt16;
                _union.UInt16 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ushort value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ushort(in Value value) => value.As<ushort>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ushort? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ushort?(in Value value) => value.As<ushort?>();
        #endregion

        #region UInt32

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(uint value)
        {
            this = default;
            _object = TypeFlags.UInt32;
            _union.UInt32 = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(uint? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt32;
                _union.UInt32 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(uint value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator uint(in Value value) => value.As<uint>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(uint? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator uint?(in Value value) => value.As<uint?>();
        #endregion

        #region UInt64

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(ulong value)
        {
            this = default;
            _object = TypeFlags.UInt64;
            _union.UInt64 = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(ulong? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt64;
                _union.UInt64 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ulong value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ulong(in Value value) => value.As<ulong>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ulong? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ulong?(in Value value) => value.As<ulong?>();
        #endregion

        #region Single

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(float value)
        {
            this = default;
            _object = TypeFlags.Single;
            _union.Single = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(float? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Single;
                _union.Single = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(float value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator float(in Value value) => value.As<float>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(float? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator float?(in Value value) => value.As<float?>();
        #endregion

        #region Double

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(double value)
        {
            this = default;
            _object = TypeFlags.Double;
            _union.Double = value;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(double? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Double;
                _union.Double = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(double value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator double(in Value value) => value.As<double>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(double? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator double?(in Value value) => value.As<double?>();
        #endregion

        #region DateTimeOffset

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(DateTimeOffset value)
        {
            this = default;
            TimeSpan offset = value.Offset;
            if (offset.Ticks == 0)
            {
                // This is a UTC time
                _union.Ticks = value.Ticks;
                _object = TypeFlags.DateTimeOffset;
            }
            else if (PackedDateTimeOffset.TryCreate(value, offset, out var packed))
            {
                _union.PackedDateTimeOffset = packed;
                _object = TypeFlags.PackedDateTimeOffset;
            }
            else
            {
                _object = value;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(DateTimeOffset? value)
        {
            this = default;
            if (!value.HasValue)
            {
                _object = null;
            }
            else
            {
                this = new(value.Value);
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(DateTimeOffset value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator DateTimeOffset(in Value value) => value.As<DateTimeOffset>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(DateTimeOffset? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator DateTimeOffset?(in Value value) => value.As<DateTimeOffset?>();
        #endregion

        #region DateTime

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(DateTime value)
        {
            this = default;

            _union.DateTime = value;
            _object = TypeFlags.DateTime;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public Value(DateTime? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.DateTime;
                _union.DateTime = value.Value;
            }
            else
            {
                _object = value;
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(DateTime value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator DateTime(in Value value) => value.As<DateTime>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(DateTime? value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator DateTime?(in Value value) => value.As<DateTime?>();
        #endregion

        #region ArraySegment

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="segment"></param>
        public Value(ArraySegment<byte> segment)
        {
            this = default;
            byte[]? array = segment.Array;
            if (array is null)
            {
                ThrowArgumentNull(nameof(segment));
            }

            _object = array;
            if (segment.Offset == 0 && segment.Count == 0)
            {
                _union.UInt64 = ulong.MaxValue;
            }
            else
            {
                _union.Segment = (segment.Offset, segment.Count);
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ArraySegment<byte> value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ArraySegment<byte>(in Value value) => value.As<ArraySegment<byte>>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="segment"></param>
        public Value(ArraySegment<char> segment)
        {
            this = default;
            char[]? array = segment.Array;
            if (array is null)
            {
                ThrowArgumentNull(nameof(segment));
            }

            _object = array;
            if (segment.Offset == 0 && segment.Count == 0)
            {
                _union.UInt64 = ulong.MaxValue;
            }
            else
            {
                _union.Segment = (segment.Offset, segment.Count);
            }
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(ArraySegment<char> value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator ArraySegment<char>(in Value value) => value.As<ArraySegment<char>>();
        #endregion

        #region Decimal

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(decimal value) => new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator decimal(in Value value) => value.As<decimal>();

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Value(decimal? value) => value.HasValue ? new(value.Value) : new(value);

        /// <summary>
        /// TBD.
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator decimal?(in Value value) => value.As<decimal?>();
        #endregion

        #region T
        /// <summary>
        /// TBD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Value Create<T>(T value)
        {
            // Explicit cast for types we don't box
            if (typeof(T) == typeof(bool)) return new(Unsafe.As<T, bool>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(byte)) return new(Unsafe.As<T, byte>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(sbyte)) return new(Unsafe.As<T, sbyte>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(char)) return new(Unsafe.As<T, char>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(short)) return new(Unsafe.As<T, short>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(int)) return new(Unsafe.As<T, int>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(long)) return new(Unsafe.As<T, long>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ushort)) return new(Unsafe.As<T, ushort>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(uint)) return new(Unsafe.As<T, uint>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ulong)) return new(Unsafe.As<T, ulong>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(float)) return new(Unsafe.As<T, float>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(double)) return new(Unsafe.As<T, double>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTime)) return new(Unsafe.As<T, DateTime>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTimeOffset)) return new(Unsafe.As<T, DateTimeOffset>(ref Unsafe.AsRef(value)));

            if (typeof(T) == typeof(bool?)) return new(Unsafe.As<T, bool?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(byte?)) return new(Unsafe.As<T, byte?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(sbyte?)) return new(Unsafe.As<T, sbyte?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(char?)) return new(Unsafe.As<T, char?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(short?)) return new(Unsafe.As<T, short?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(int?)) return new(Unsafe.As<T, int?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(long?)) return new(Unsafe.As<T, long?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ushort?)) return new(Unsafe.As<T, ushort?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(uint?)) return new(Unsafe.As<T, uint?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ulong?)) return new(Unsafe.As<T, ulong?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(float?)) return new(Unsafe.As<T, float?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(double?)) return new(Unsafe.As<T, double?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTime?)) return new(Unsafe.As<T, DateTime?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTimeOffset?)) return new(Unsafe.As<T, DateTimeOffset?>(ref Unsafe.AsRef(value)));

            if (typeof(T) == typeof(ArraySegment<byte>)) return new(Unsafe.As<T, ArraySegment<byte>>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ArraySegment<char>)) return new(Unsafe.As<T, ArraySegment<char>>(ref Unsafe.AsRef(value)));

            if (typeof(T).IsEnum)
            {
                Debug.Assert(Unsafe.SizeOf<T>() <= sizeof(ulong));
                return new Value(StraightCastFlag<T>.Instance, Unsafe.As<T, ulong>(ref value));
            }

            return new Value(value);
        }

        private Value(object o, ulong u)
        {
            _union = default;
            _object = o;
            _union.UInt64 = u;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryGetValue<T>(out T value)
        {
            bool success;

            // Checking the type gets all of the non-relevant compares elided by the JIT
            if (_object is not null && ((typeof(T) == typeof(bool) && _object == TypeFlags.Boolean)
                || (typeof(T) == typeof(byte) && _object == TypeFlags.Byte)
                || (typeof(T) == typeof(char) && _object == TypeFlags.Char)
                || (typeof(T) == typeof(double) && _object == TypeFlags.Double)
                || (typeof(T) == typeof(short) && _object == TypeFlags.Int16)
                || (typeof(T) == typeof(int) && _object == TypeFlags.Int32)
                || (typeof(T) == typeof(long) && _object == TypeFlags.Int64)
                || (typeof(T) == typeof(sbyte) && _object == TypeFlags.SByte)
                || (typeof(T) == typeof(float) && _object == TypeFlags.Single)
                || (typeof(T) == typeof(ushort) && _object == TypeFlags.UInt16)
                || (typeof(T) == typeof(uint) && _object == TypeFlags.UInt32)
                || (typeof(T) == typeof(ulong) && _object == TypeFlags.UInt64)))
            {
                value = CastTo<T>();
                success = true;
            }
            else if (typeof(T) == typeof(DateTime) && _object == TypeFlags.DateTime)
            {
                value = Unsafe.As<DateTime, T>(ref Unsafe.AsRef(_union.DateTime));
                success = true;
            }
            else if (typeof(T) == typeof(DateTimeOffset) && _object == TypeFlags.DateTimeOffset)
            {
                value = Unsafe.As<DateTimeOffset, T>(ref Unsafe.AsRef(new DateTimeOffset(_union.Ticks, TimeSpan.Zero)));
                success = true;
            }
            else if (typeof(T) == typeof(DateTimeOffset) && _object == TypeFlags.PackedDateTimeOffset)
            {
                value = Unsafe.As<DateTimeOffset, T>(ref Unsafe.AsRef(_union.PackedDateTimeOffset.Extract()));
                success = true;
            }
            else if (typeof(T).IsValueType)
            {
                success = TryGetValueSlow(out value);
            }
            else
            {
                success = TryGetObjectSlow(out value);
            }

            return success;
        }

        private readonly bool TryGetValueSlow<T>(out T value)
        {
            // Single return has a significant performance benefit.

            bool result = false;

            if (_object is null)
            {
                // A null is stored, it can only be assigned to a reference type or nullable.
                value = default!;
                result = Nullable.GetUnderlyingType(typeof(T)) is not null;
            }
            else if (typeof(T).IsEnum && _object is TypeFlag<T> typeFlag)
            {
                value = typeFlag.To(in this);
                result = true;
            }
            else if (_object is T t)
            {
                value = t;
                result = true;
            }
            else if (typeof(T) == typeof(ArraySegment<byte>))
            {
                ulong bits = _union.UInt64;
                if (bits != 0 && _object is byte[] byteArray)
                {
                    ArraySegment<byte> segment = bits != ulong.MaxValue
                        ? new(byteArray, _union.Segment.Offset, _union.Segment.Count)
                        : new(byteArray, 0, 0);
                    value = Unsafe.As<ArraySegment<byte>, T>(ref segment);
                    result = true;
                }
                else
                {
                    value = default!;
                }
            }
            else if (typeof(T) == typeof(ArraySegment<char>))
            {
                ulong bits = _union.UInt64;
                if (bits != 0 && _object is char[] charArray)
                {
                    ArraySegment<char> segment = bits != ulong.MaxValue
                        ? new(charArray, _union.Segment.Offset, _union.Segment.Count)
                        : new(charArray, 0, 0);
                    value = Unsafe.As<ArraySegment<char>, T>(ref segment);
                    result = true;
                }
                else
                {
                    value = default!;
                }
            }
            else if (typeof(T) == typeof(int?) && _object == TypeFlags.Int32)
            {
                value = Unsafe.As<int?, T>(ref Unsafe.AsRef((int?)_union.Int32));
                result = true;
            }
            else if (typeof(T) == typeof(long?) && _object == TypeFlags.Int64)
            {
                value = Unsafe.As<long?, T>(ref Unsafe.AsRef((long?)_union.Int64));
                result = true;
            }
            else if (typeof(T) == typeof(bool?) && _object == TypeFlags.Boolean)
            {
                value = Unsafe.As<bool?, T>(ref Unsafe.AsRef((bool?)_union.Boolean));
                result = true;
            }
            else if (typeof(T) == typeof(float?) && _object == TypeFlags.Single)
            {
                value = Unsafe.As<float?, T>(ref Unsafe.AsRef((float?)_union.Single));
                result = true;
            }
            else if (typeof(T) == typeof(double?) && _object == TypeFlags.Double)
            {
                value = Unsafe.As<double?, T>(ref Unsafe.AsRef((double?)_union.Double));
                result = true;
            }
            else if (typeof(T) == typeof(uint?) && _object == TypeFlags.UInt32)
            {
                value = Unsafe.As<uint?, T>(ref Unsafe.AsRef((uint?)_union.UInt32));
                result = true;
            }
            else if (typeof(T) == typeof(ulong?) && _object == TypeFlags.UInt64)
            {
                value = Unsafe.As<ulong?, T>(ref Unsafe.AsRef((ulong?)_union.UInt64));
                result = true;
            }
            else if (typeof(T) == typeof(char?) && _object == TypeFlags.Char)
            {
                value = Unsafe.As<char?, T>(ref Unsafe.AsRef((char?)_union.Char));
                result = true;
            }
            else if (typeof(T) == typeof(short?) && _object == TypeFlags.Int16)
            {
                value = Unsafe.As<short?, T>(ref Unsafe.AsRef((short?)_union.Int16));
                result = true;
            }
            else if (typeof(T) == typeof(ushort?) && _object == TypeFlags.UInt16)
            {
                value = Unsafe.As<ushort?, T>(ref Unsafe.AsRef((ushort?)_union.UInt16));
                result = true;
            }
            else if (typeof(T) == typeof(byte?) && _object == TypeFlags.Byte)
            {
                value = Unsafe.As<byte?, T>(ref Unsafe.AsRef((byte?)_union.Byte));
                result = true;
            }
            else if (typeof(T) == typeof(sbyte?) && _object == TypeFlags.SByte)
            {
                value = Unsafe.As<sbyte?, T>(ref Unsafe.AsRef((sbyte?)_union.SByte));
                result = true;
            }
            else if (typeof(T) == typeof(DateTime?) && _object == TypeFlags.DateTime)
            {
                value = Unsafe.As<DateTime?, T>(ref Unsafe.AsRef((DateTime?)_union.DateTime));
                result = true;
            }
            else if (typeof(T) == typeof(DateTimeOffset?) && _object == TypeFlags.DateTimeOffset)
            {
                value = Unsafe.As<DateTimeOffset?, T>(ref Unsafe.AsRef((DateTimeOffset?)new DateTimeOffset(_union.Ticks, TimeSpan.Zero)));
                result = true;
            }
            else if (typeof(T) == typeof(DateTimeOffset?) && _object == TypeFlags.PackedDateTimeOffset)
            {
                value = Unsafe.As<DateTimeOffset?, T>(ref Unsafe.AsRef((DateTimeOffset?)_union.PackedDateTimeOffset.Extract()));
                result = true;
            }
            else if (Nullable.GetUnderlyingType(typeof(T)) is Type underlyingType
                && underlyingType.IsEnum
                && _object is TypeFlag underlyingTypeFlag
                && underlyingTypeFlag.Type == underlyingType)
            {
                // Asked for a nullable enum and we've got that type.

                // We've got multiple layouts, depending on the size of the enum backing field. We can't use the
                // nullable itself (e.g. default(T)) as a template as it gets treated specially by the runtime.

                int size = Unsafe.SizeOf<T>();

                switch (size)
                {
                    case (2):
                        value = Unsafe.As<NullableTemplate<byte>, T>(ref Unsafe.AsRef(new NullableTemplate<byte>(_union.Byte)));
                        result = true;
                        break;
                    case (4):
                        value = Unsafe.As<NullableTemplate<ushort>, T>(ref Unsafe.AsRef(new NullableTemplate<ushort>(_union.UInt16)));
                        result = true;
                        break;
                    case (8):
                        value = Unsafe.As<NullableTemplate<uint>, T>(ref Unsafe.AsRef(new NullableTemplate<uint>(_union.UInt32)));
                        result = true;
                        break;
                    case (16):
                        value = Unsafe.As<NullableTemplate<ulong>, T>(ref Unsafe.AsRef(new NullableTemplate<ulong>(_union.UInt64)));
                        result = true;
                        break;
                    default:
                        ThrowInvalidOperation();
                        value = default!;
                        result = false;
                        break;
                }
            }
            else
            {
                value = default!;
                result = false;
            }

            return result;
        }

        private readonly bool TryGetObjectSlow<T>(out T value)
        {
            // Single return has a significant performance benefit.

            bool result = false;

            if (_object is null)
            {
                value = default!;
            }
            else if (typeof(T) == typeof(char[]))
            {
                if (_union.UInt64 == 0 && _object is char[])
                {
                    value = (T)_object;
                    result = true;
                }
                else
                {
                    // Don't allow "implicit" cast to array if we stored a segment.
                    value = default!;
                    result = false;
                }
            }
            else if (typeof(T) == typeof(byte[]))
            {
                if (_union.UInt64 == 0 && _object is byte[])
                {
                    value = (T)_object;
                    result = true;
                }
                else
                {
                    // Don't allow "implicit" cast to array if we stored a segment.
                    value = default!;
                    result = false;
                }
            }
            else if (typeof(T) == typeof(object))
            {
                // This case must also come before the _object is T case to make sure we don't leak our flags.
                if (_object is TypeFlag flag)
                {
                    value = (T)flag.ToObject(this);
                    result = true;
                }
                else if (_union.UInt64 != 0 && _object is char[] chars)
                {
                    value = _union.UInt64 != ulong.MaxValue
                        ? (T)(object)new ArraySegment<char>(chars, _union.Segment.Offset, _union.Segment.Count)
                        : (T)(object)new ArraySegment<char>(chars, 0, 0);
                    result = true;
                }
                else if (_union.UInt64 != 0 && _object is byte[] bytes)
                {
                    value = _union.UInt64 != ulong.MaxValue
                        ? (T)(object)new ArraySegment<byte>(bytes, _union.Segment.Offset, _union.Segment.Count)
                        : (T)(object)new ArraySegment<byte>(bytes, 0, 0);
                    result = true;
                }
                else
                {
                    value = (T)_object;
                    result = true;
                }
            }
            else if (_object is T t)
            {
                value = t;
                result = true;
            }
            else
            {
                value = default!;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// TBD.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T As<T>()
        {
            if (!TryGetValue<T>(out T value))
            {
                ThrowInvalidCast();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly T CastTo<T>()
        {
            Debug.Assert(typeof(T).IsPrimitive);
            T value = Unsafe.As<Union, T>(ref Unsafe.AsRef(_union));
            return value;
        }
        #endregion
    }
}
