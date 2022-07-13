using System;

namespace Tests.Examples.CSharp 
{
	[Title("My Example Test")]
	public class ExampleTest : WAT.Test
	{
		[Test]
		public void MySimpleTest()
		{
			Describe("This Is My Simple Test");
			Assert.IsTrue(true, "Optional Context Argument");
		}
	}
}