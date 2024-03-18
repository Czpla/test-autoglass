namespace App.Shared.Either
{
    using System;
    using System.Collections.Generic;

    public class Result<T, E>
    {
        public T Value { get; } = default(T)!;
        public E Error { get; } = default(E)!;

        public Result(T value) => Value = value;
        public Result(E error) => Error = error;

        /// <summary>
        ///     Retorna `true` se o resultado for [`Ok`].
        /// </summary>
        public bool IsOk() => !EqualityComparer<T>.Default.Equals(Value, default(T));

        /// <summary>
        ///     Retorna `true` se o resultado for [`Ok`] e o valor passar no predicado.
        /// </summary>
        public bool IsOkAnd(Predicate<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return IsOk() && predicate(Value);
        }

        /// <summary>
        ///     Retorna `true` se o resultado for [`Err`].
        /// </summary>
        public bool IsError() => !IsOk();

        /// <summary>
        ///     Retorna `true` se o resultado for [`Err`] e o valor passar no predicado.
        /// </summary>
        public bool IsErrorAnd(Predicate<E> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return IsError() && predicate(Error);
        }

        public U Match<U>(Func<T, U> ok, Func<E, U> error)
        {
            if (ok is null)
                throw new ArgumentNullException(nameof(ok));

            if (error is null)
                throw new ArgumentNullException(nameof(error));

            return IsOk() ? ok(Value) : error(Error);
        }

        public Result<U, E> Bind<U>(Func<T, Result<U, E>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return IsOk() ? predicate(Value) : new Result<U, E>(Error);
        }

        /// <summary>
        ///    Mapeia um `Result<T, E>` para `Result<U, E>` aplicando uma função ao valor
        ///    deixando o erro inalterado.
        /// </summary>
        public Result<U, E> Map<U>(Func<T, U> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Bind(
                value => new Result<U, E>(predicate(value))
            );
        }

        /// <summary>
        ///   Mapeia um `Result<T, E>` para `Result<T, F>` aplicando uma função ao erro
        /// </summary>
        public T MapOr(T defaultValue, Func<T, T> mapper)
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            if (EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                throw new ArgumentNullException(nameof(defaultValue));

            return IsOk() ? mapper(Value) : defaultValue;
        }

        /// <summary>
        ///   Mapeia um `Result<T, E>` para `Result<T, F>` aplicando uma função ao erro
        /// </summary>
        public Result<T, F> MapError<F>(Func<E, F> mapper)
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            return IsOk() ? new Result<T, F>(Value) : new Result<T, F>(mapper(Error));
        }

        public T Unwrap()
        {
            if (IsError())
                throw new InvalidOperationException("Cannot unwrap an error");

            return Value;
        }
        public T UnwrapOr(T defaultValue)
        {
            if (EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                throw new ArgumentNullException(nameof(defaultValue));

            return Match(
                ok: value => value,
                error: _ => defaultValue
            );
        }
        public T UnwrapOrElse(Func<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Match(
                ok: value => value,
            error: _ => predicate()
            );
        }
        public T UnwrapOrDefault()
        {
            return Match(
                ok: value => value,
                error: _ => default(T)!
            );
        }
        public T UnwrapAndThen(Func<T, T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Match(
                ok: value => predicate(value),
                error: _ => default(T)!
            );
        }

        public static implicit operator Result<T, E>(T value) => new Result<T, E>(value);
        public static implicit operator Result<T, E>(E error) => new Result<T, E>(error);

        public static Result<T, List<E>> Combine(params Result<T, E>[] results)
        {
            if (results is null)
                throw new ArgumentNullException(nameof(results));

            if (results.Length == 0)
                throw new ArgumentException("The results array cannot be empty", nameof(results));

            var errors = new List<E>();

            foreach (var result in results)
            {
                if (result is null)
                    throw new ArgumentNullException(nameof(results));

                if (result.IsError())
                    errors.Add(result.Error);
            }

            if (errors.Count == 0)
                return Result<T, List<E>>.Ok(results[0].Value);

            return Result<T, List<E>>.Err(errors);
        }

        public static Result<T, E> Ok(T value) => new Result<T, E>(value);
        public static Result<T, E> Err(E error) => new Result<T, E>(error);
    }
}