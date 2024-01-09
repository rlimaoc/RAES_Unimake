using Uni.NFCom.Servicos.Enums;
using Uni.NFCom.Xml.NFCom;
using Xunit;

namespace Unimake.DFe.Test.BugFixes
{
    public class Bug140668
    {
        #region Public Methods

        [Fact]
        [Trait("Bug", "140668")]
        public void SetXCplToNullThrowsArgumentNullException()
        {
            var enderEmit = new EnderEmit
            {
                XLgr = "lorem ipsum dolor",
                Nro = "123",
                XCpl = null,
                XBairro = "lorem ipsum",
                CMun = 9999999,
                XMun = "lorem",
                UF = UFBrasil.PR,
                CEP = "00000-000"
            };

            Assert.Null(enderEmit.XCpl);
            
        }

        #endregion Public Methods
    }
}