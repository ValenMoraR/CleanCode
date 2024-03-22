namespace PA_Refactoring_Test
{
    [TestClass]
    public class Principal_Test
    {
        [TestMethod]
        public void TestAgregarUsuario()
        {
            //Arrange
            Cliente cliente = new Cliente
            {
                Cedula = "3145",
                Estrato = 3,
                MetaAhorro = 150,
                ConsumoActualEnergia = 180,
                ConsumoActualAgua = 20
            };

            AgregarUsuario();
            Assert.IsTrue(clientes.Contains(cliente));
        }
    }
}