using CorrugatedIron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiakIronBrowser
{
    public class RiakBase
    {
        IRiakClient client = null;

        public RiakBase()
        {
            var cluster = RiakCluster.FromConfig("riakConfig");
            client = cluster.CreateClient();
        }

        public IList<string> GetAllBuckets()
        {
            var result = client.ListBuckets();
            return result.Value.ToList();
        }


        public IList<string> GetAllKeys(string bucket)
        {
            var result = client.ListKeys(bucket);
            return result.Value.ToList();
        }

        /// <summary>
        /// Deletes a complete bucket. Actually deletes all the keys in the bucket effectively deleting said bucket.
        /// </summary>
        /// <param name="bucket">Name of bucket to delete.</param>
        /// <returns>Errors returned</returns>
        public IList<string> DeleteBucket(string bucket)
        {
            var results = client.DeleteBucket(bucket);

            IList<string> errorList = new List<string>();
            foreach (var result in results)
                if (!result.IsSuccess)
                    errorList.Add(result.ErrorMessage);

            return errorList;
        }

        public void DeleteKey(string bucket, string key)
        {
            var result = client.Delete(bucket, key);

            if (!result.IsSuccess)
                throw new Exception(result.ErrorMessage);
        }
    }
}
