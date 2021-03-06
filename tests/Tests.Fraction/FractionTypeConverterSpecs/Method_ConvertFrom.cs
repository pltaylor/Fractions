﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using Fractions;
using NUnit.Framework;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace Tests.Fractions.FractionTypeConverterSpecs.Methods_ConvertFrom
{
    [TestFixture]
    public class If_the_user_wants_to_convert_a_type_to_Fraction : Spec
    {
        private FractionTypeConverter _converter;

        public override void SetUp() {
            _converter = new FractionTypeConverter();
        }

        private static IEnumerable TypeTests {
            get {
                yield return new TestCaseData(typeof(Fraction)).Returns(true);
                yield return new TestCaseData(typeof(int)).Returns(true);
                yield return new TestCaseData(typeof(long)).Returns(true);
                yield return new TestCaseData(typeof(decimal)).Returns(true);
                yield return new TestCaseData(typeof(double)).Returns(true);
                yield return new TestCaseData(typeof(string)).Returns(true);
                yield return new TestCaseData(typeof(BigInteger)).Returns(true);

                yield return new TestCaseData(typeof(bool)).Returns(false);
                yield return new TestCaseData(typeof(object)).Returns(false);
            }
        }

        [Test, TestCaseSource("TypeTests")]
        public bool Shall_the_result_of_CanConvertFrom_be_correct(Type destination_type) {
            return _converter.CanConvertFrom(destination_type);
        }

        private static IEnumerable ConvertFromTests {
            get {
                yield return new TestCaseData(Fraction.One, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(new Fraction(1, 2), CultureInfo.CurrentCulture).Returns(new Fraction(1, 2));

                yield return new TestCaseData(0, CultureInfo.CurrentCulture).Returns(Fraction.Zero);
                yield return new TestCaseData(1, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(-1, CultureInfo.CurrentCulture).Returns(new Fraction(-1));

                yield return new TestCaseData(BigInteger.Zero, CultureInfo.CurrentCulture).Returns(Fraction.Zero);
                yield return new TestCaseData(BigInteger.One, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(BigInteger.MinusOne, CultureInfo.CurrentCulture).Returns(new Fraction(-1));

                yield return new TestCaseData(1L, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(0L, CultureInfo.CurrentCulture).Returns(Fraction.Zero);

                yield return new TestCaseData(1m, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(0.5m, CultureInfo.CurrentCulture).Returns(new Fraction(1, 2));

                yield return new TestCaseData(1d, CultureInfo.CurrentCulture).Returns(Fraction.One);
                yield return new TestCaseData(0.5d, CultureInfo.CurrentCulture).Returns(new Fraction(1, 2));

                yield return new TestCaseData("1", CultureInfo.CurrentCulture).Returns(new Fraction(1));
                yield return new TestCaseData("123", CultureInfo.CurrentCulture).Returns(new Fraction(123));
                yield return new TestCaseData("-1", CultureInfo.CurrentCulture).Returns(new Fraction(-1));
                yield return new TestCaseData("1/2", CultureInfo.CurrentCulture).Returns(new Fraction(1, 2));
                yield return new TestCaseData("-1/2", CultureInfo.CurrentCulture).Returns(new Fraction(-1, 2));
                yield return new TestCaseData("1/-2", CultureInfo.CurrentCulture).Returns(new Fraction(-1, 2));
                yield return new TestCaseData("-1/-2", CultureInfo.CurrentCulture).Returns(new Fraction(1, 2));
                yield return new TestCaseData("0,5", CultureInfo.GetCultureInfo("de-DE")).Returns(new Fraction(1, 2));
                yield return new TestCaseData("-0,5", CultureInfo.GetCultureInfo("de-DE")).Returns(new Fraction(-1, 2));
                yield return new TestCaseData("0.5", CultureInfo.GetCultureInfo("en-US")).Returns(new Fraction(1, 2));
                yield return new TestCaseData("-0.5", CultureInfo.GetCultureInfo("en-US")).Returns(new Fraction(-1, 2));
                yield return new TestCaseData("-0.125", CultureInfo.GetCultureInfo("en-US")).Returns(new Fraction(-1, 8));

                yield return new TestCaseData(null, CultureInfo.CurrentCulture).Returns(Fraction.Zero);
            }
        }

        [Test, TestCaseSource("ConvertFromTests")]
        public Fraction Shall_the_result_of_ConvertFrom_be_correct(object value, CultureInfo culture_info) {
            // ReSharper disable once PossibleNullReferenceException
            var result = (Fraction)_converter.ConvertFrom(null, culture_info, value);
            Debug.Print("Type: {0}, Result: {1}", value != null ? value.GetType() : null, result);

            return result;
        }
    }
}