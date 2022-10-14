using Lesson9;

namespace TestProject1
{
    public class Lesson9Test
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// ��� ����� ��� �������� ������� �1 - ������� �����
        /// </summary>
        [Test]
        public void Solution1_3_TrueReturned()
        {
            // arrange
            int number = 3;
            bool expected_primenumber = true;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution1(number, true);

            // assert
            Assert.That(actual, Is.EqualTo(expected_primenumber));

        }

        [Test]
        public void Solution1_8_FalseReturned()
        {
            // arrange
            int number = 8;
            bool expected_primenumber = false;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution1(number, true);

            // assert
            Assert.That(actual, Is.EqualTo(expected_primenumber));

        }

        /// <summary>
        /// ��� ����� ��� �������� ������� �2 - ���������� ���
        /// </summary>
        [Test]
        public void Solution2_2002_FalseReturned()
        {
            // arrange
            int year = 2002;
            bool expected_flag_exception = false;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution2(year, true);

            // assert
            Assert.That(actual, Is.EqualTo(expected_flag_exception));
        }

        [Test]
        public void Solution2_2004_TrueReturned()
        {
            // arrange
            int year = 2004;
            bool expected_flag_exception = true;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution2(year, true);

            // assert
            Assert.That(actual, Is.EqualTo(expected_flag_exception));
        }


        /// <summary>
        /// ��� ����� ��� �������� ������� �4 - ����� � ����������
        /// </summary>
        [Test]
        public void Solution4_1_1_FalseReturned()
        {
            // arrange
            int XPoint = 1;
            int YPoint = 1;
            bool flagTest = false;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution4(XPoint, YPoint, true);

            // assert
            Assert.That(actual, Is.EqualTo(flagTest));
        }

        [Test]
        public void Solution4_0_0_TrueReturned()
        {
            // arrange
            int XPoint = 0;
            int YPoint = 0;
            bool flagTest = true;

            // act
            Tasks sol = new Tasks();
            bool actual = sol.Solution4(XPoint, YPoint, true);

            // assert
            Assert.That(actual, Is.EqualTo(flagTest));
        }

    }
}