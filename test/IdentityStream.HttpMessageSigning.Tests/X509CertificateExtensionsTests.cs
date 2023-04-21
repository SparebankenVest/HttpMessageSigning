using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace IdentityStream.HttpMessageSigning.Tests {
    [UsesVerify]
    public class X509CertificateExtensionsTests
    {
        private const string Certificate =
            "MIIEcgIBAzCCBC4GCSqGSIb3DQEHAaCCBB8EggQbMIIEFzCCAdgGCSqGSIb3DQEHAaCCAckEggHFMIIBwTCCAb0GCyqGSIb3DQEMCgECoIIBNjCCATIwHAYKKoZIhvcNAQwBAzAOBAgr8FgwcfOvJgICB9AEggEQO8ZAEIB8+u08rn/85hdyhssyjv4prIHSGjXn8YkbLjR4RTHadjNyCUotLjAFykFA+ECkIbZsUFZ3Tm4FAhqnGJy+u+zmZ/Gjk6strduHuQiMbyblCtit/L8EZVkFSeD55K0tkRFs/b4Md/ThpkJ9vOOjBmOcpRoX5+igyEgEmLItDNdhekuVZuIiPXNhR4/eAlOu5jnnLa7UFq45Qiy1s1jiieEvtvsfKGCDdoNhhbqFokE+UO9hQyR5IHk1jBE3FBmJfjDdVd2y481tBaZgFDhVy1/noBVhDzUEYJPnUdeiul2Z56Y1EQbUDM0cJlIulfBBK3E5VXr3DnVF9JMX78XH1BQfkRs8WT01Q+ZH6LoxdDATBgkqhkiG9w0BCRUxBgQEAQAAADBdBgkrBgEEAYI3EQExUB5OAE0AaQBjAHIAbwBzAG8AZgB0ACAAUwBvAGYAdAB3AGEAcgBlACAASwBlAHkAIABTAHQAbwByAGEAZwBlACAAUAByAG8AdgBpAGQAZQByMIICNwYJKoZIhvcNAQcGoIICKDCCAiQCAQAwggIdBgkqhkiG9w0BBwEwHAYKKoZIhvcNAQwBAzAOBAg0uW7mY0eSQAICB9CAggHwKuet+VWPtVZgoVAkU6/OqEpJZh31f1yOpWzKyy/5ZEnftZWMT3yoAMhOcFGKgrqmlOhfpW8TUznFnOzTy413uaPPdw90kFhgB/n3fED3d+FdURW6zYpKBTJOGakeDEGBc5pNE4wDdNO7Li0MvFLqYQIVO1bBYje4bYK/TQctTIiq3vYJKVin2KihWoCEVj5cfzH8FtR5XAwbSRGr/9FglOuUxYncFsXewglAo9wTjlLHrdaWnlzdBP32yaPrm+jl3N7J/JE48l8uMkutLTvzICNMtQ9JySpwZvFamzgZ/21J/3m9dHUxKn+qm8GFG1rn8hiYNUHezGFDAesyd+4W/IFX6IM4eL5bkb5iLAI5C9au5/skZ1L7bV89CgPFBBPbYhgzU5Nu3uAl15ja/fJXkQJPN4MpWGn8bRG+PFgwRZ/bMlnGFXpY0LyU3La3VxB5ina6tJcnrWCt6nGOlqEA9oW6sf4C5nLqEoWuxZY+a72EEdxslZr8Abfl10jsZH8eTbeLllEpCshhSiZHlAtTRx3/CgT/6RAaPfjIaNVQqCt9m8arkDj2b1YvTPWbHMhA/6wycVUuH5Kw/uX/E6LhboglBOngi5dmyh73DCWEVzOxlc5ThAXAR7hX4k+u0F8ZU+uxJMmUN/1ILR6CVAtVljA7MB8wBwYFKw4DAhoEFLEmLu2SQBmTpWE2sYHYl8wwUjixBBSSNPwG5VtASQwaRL67MQppY91ufwICB9A=";
        
        [Fact]
        public async Task GetKeyId_ReturnsLowerCaseKeyId()
        {
            using var cert = new X509Certificate2(Convert.FromBase64String(Certificate));

            var keyId = cert.GetKeyId();
            
            Assert.Equal(keyId, keyId.ToLowerInvariant());
            await Verifier.Verify(keyId);
        }
    }
}
 