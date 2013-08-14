using System;

namespace Intrigma.DonorSpace.Infrastructure.Helper
{
    public static class GuidComb
    {
        /// <summary>
        /// Creates a new sequential guid (aka comb) <see cref="http://www.informit.com/articles/article.aspx?p=25862&seqNum=7"/>.
        /// The only other way to resolve that issue is to recreate constraint on guid column with newsequentialguid() and use
        /// DatabaseGenerateOption.Identity
        /// <see cref="http://entityframework.codeplex.com/discussions/403855"/>
        /// </summary>
        /// <remarks>A comb provides the benefits of a standard Guid w/o the database performance problems.</remarks>
        /// <returns>A new sequential guid (comb).</returns>
        public static Guid Generate()
        {
            var guidBinary = new byte[16];
            Array.Copy(Guid.NewGuid().ToByteArray(), 0, guidBinary, 0, 8);
            Array.Copy(BitConverter.GetBytes(DateTime.Now.Ticks), 0, guidBinary, 8, 8);
            return new Guid(guidBinary);
        }
    }
}
