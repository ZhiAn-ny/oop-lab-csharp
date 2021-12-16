namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double _re;
        private readonly double _im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this._re = re;
            this._im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => _re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => _im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan(Imaginary / Real);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            // TODO improve
            String str = Real.Equals(0) ? "" : Real.ToString();
            if (!Imaginary.Equals(0))
            {
                Double abs = Math.Abs(Imaginary);
                if (abs.Equals(1))
                {
                    return abs > 0 ? str + "+i" : str + "-i";
                }
                return abs > 0 ? str + "+" + Imaginary + "i" : str + Imaginary + "i";
            }
            return Real.ToString();
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            if (obj is IComplex)
                return GetHashCode().Equals(obj.GetHashCode());
            return false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

    }
}
