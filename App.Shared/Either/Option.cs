namespace App.Shared.Either
{
    using System;
    using System.Collections.Generic;

    public class Option<T> where T : notnull
    {
        public T Value { get; } = default(T)!;

        public Option(T value) => Value = value;

        public bool IsSome() => !EqualityComparer<T>.Default.Equals(Value, default(T));
        public bool IsSomeAnd(Predicate<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return IsSome() && predicate(Value);
        }
        public bool IsNone() => !IsSome();
        public bool IsNoneAnd(Predicate<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return IsNone() && predicate(Value);
        }

        /// <summary>
        ///    Executa as funções de acordo com o valor do `Option<T>`.
        /// </summary>
        public U Match<U>(Func<T, U> some, Func<U> none)
        {
            if (some is null)
                throw new ArgumentNullException(nameof(some));

            if (none is null)
                throw new ArgumentNullException(nameof(none));

            return IsSome() ? some(Value) : none();
        }

        // <summary>
        //    Executa o predicado se o valor for [`Some`],
        /// </summary>
        public Option<U> Bind<U>(Func<T, Option<U>> predicate) where U : notnull
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return this.Match(
                some: predicate,
                none: () => new Option<U>(default(U)!)
            );
        }

        // <summary>
        //    Mapeia um `Option<T>` para `Option<U>` aplicando uma função ao valor
        /// </summary>
        public Option<U> Map<U>(Func<T, U> mapper) where U : notnull
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            return Bind(
                value => Option<U>.Some(mapper(value))
            );
        }

        // <summary>
        //     Retorna o valor do `Option<T>` ou o valor padrão.
        /// </summary>
        public T MapOr(T defaultValue, Func<T, T> mapper)
        {
            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            if (EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                throw new ArgumentNullException(nameof(defaultValue));

            return IsSome() ? mapper(Value) : defaultValue;
        }

        /// <summary>
        ///    Filtra o valor do `Option<T>` aplicando um predicado.
        ///    caso o predicado retorne `false` o valor é [`None`].
        ///    caso o predicado retorne `true` o valor é [`Some`].
        /// </summary>
        public Option<T> Filter(Predicate<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Bind(value => predicate(value) ? this : Option<T>.None);
        }

        public T Unwrap()
        {
            return Match(
                some: value => value,
                none: () => default(T)!
            );
        }
        public T UnwrapOr(T defaultValue)
        {
            if (EqualityComparer<T>.Default.Equals(defaultValue, default(T)))
                throw new ArgumentNullException(nameof(defaultValue));

            return Match(
                some: value => value,
                none: () => defaultValue
            );
        }
        public T UnwrapOrElse(Func<T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Match(
                some: value => value,
                none: () => predicate()
            );
        }
        public T UnwrapOrDefault()
        {
            return Match(
                some: value => value,
                none: () => default(T)!
            );
        }
        public T UnwrapAndThen(Func<T, T> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return Match(
                some: value => predicate(value),
                none: () => default(T)!
            );
        }

        public static Option<T> Some(T value) => new Option<T>(value);

        public static Option<T> None => new Option<T>(default(T)!);
    }
}