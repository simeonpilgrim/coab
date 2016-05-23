using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Main
{
    internal class ObjectVerifier
    {
        public int VerificationCount => _delayedTests.Count;
        private readonly Queue<DelayedTests> _delayedTests = new Queue<DelayedTests>();

        public void Verify()
        {
            var exceptions = new StringBuilder();
            while(_delayedTests.Count > 0)
            {
                var delayedTest = _delayedTests.Dequeue();
                if (!delayedTest.ObjectEvaluator.Invoke())
                {
                    exceptions.AppendLine($"{delayedTest.TestExpression} failed its evaluation");
                }
            }
            if (exceptions.Length > 0)
            {
                NUnit.Framework.Assert.Fail(exceptions.ToString());
            }
        }

        internal void CheckThat(Expression<Func<bool>> evaluationTest)
        {
            var expression = evaluationTest.ToString();
            expression = Regex.Replace(expression, @"value(.+?)\)\.", "");
            _delayedTests.Enqueue(new DelayedTests
            {
                TestExpression = expression,
                ObjectEvaluator = evaluationTest.Compile()
            });
        }

        private class DelayedTests
        {
            public string TestExpression { get; set; }
            public Func<bool> ObjectEvaluator { get; set; } 
        }
    }
}
