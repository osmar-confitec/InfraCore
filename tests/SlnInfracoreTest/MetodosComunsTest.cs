using MetodosComunsApi;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SlnInfracoreTest
{

    public class ClienteTestMethodo
    {
        public string Nome { get; set; }

        public int Idade { get; set; }

        public DateTime DataNascimento { get; set; }
    }

    public class ClienteViewModelTestMethodo
    {
        public string Nome { get; set; }

        public int Idade { get; set; }

        public DateTime DataNascimento { get; set; }
    }


    public class ObjCopy
    {

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public ObjCopy ObjCopy2 { get; set; }

        public List<ObjCopy> ObjCopys { get; set; }

        public ObjCopy()
        {
            ObjCopys = new List<ObjCopy>();
        }

    }

    public class MetodosComunsTest
    {


        [Fact]
        public void CopyObjectViewModel()
        {
            //Arrange
            var cop1 = new ClienteTestMethodo { DataNascimento = DateTime.Now.AddYears(-30), Idade = 15, Nome = "Francisco Penna" };
            var cop2 = new ClienteViewModelTestMethodo { };

            // Act
            MetodosComuns.CopyT1T2DiferentClass(cop1, cop2);

            //Assert
            Assert.True(cop2.Nome.Equals(cop1.Nome));
        }


        [Fact]
        public void Copy()
        {
            //Arrange
            var cop1 = new ObjCopy { Codigo = 1, Nome = "", ObjCopys = new List<ObjCopy> { new ObjCopy {  Nome = "FelizBerto" } } , ObjCopy2 = new ObjCopy{ Nome= "Maria" } };
            var cop2 = new ObjCopy() { Nome = "Patricia" };

            // Act
            MetodosComuns.CopyT1T2(cop1, cop2, false);

            //Assert
            //Assert.True(cop2.Nome.Equals(cop1.Nome));
        }

        [Fact]
        public void DataSqlValida()
        {

            //Arrange
            var dataTest = new DateTime();
            var dataHj = DateTime.Now;
            var idade = new DateTime(1981, 7, 15);
            bool valida;


            // Act
            valida =  dataTest.DataValidaSQL();
            var datadif = new DiferencaEntreDatas(dataHj, new DateTime(1981,7,15));

            //Assert
            Assert.False(valida );
            Assert.True(datadif.Years > 18);
            Assert.True(idade.DataValidaSQL());

        }

    }
}
