namespace CloudHRMS.UnitTests {
    public class TestCalculator {
        [Fact]
        public void CheckSum3NumbersTrue() {
            //1)Arrange
            Calculator calculator = new Calculator();
            int n1 = 2, n2 = 2, n3 = 2;
            int expectedResult = 6;
            //2)Action
            int actualResult = calculator.Sum3Numbers(n1, n2, n3);
            //3)Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckSum3NumbersFalse() {
            //1)Arrange
            Calculator calculator = new Calculator();
            int n1 = 1, n2 = 1, n3 = 1;
            int expectedResult = 5;
            //2)Action
            int actualResult = calculator.Sum3Numbers(n1, n2, n3);
            //3)Assert
            Assert.NotEqual(expectedResult, actualResult);
        }
    }
}
