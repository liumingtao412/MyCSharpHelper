using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyHelper4Web
{
  public class MyRandomHelper
    {
      /// <summary>
      /// 使用Reservoir算法，产生从[0，range）范围的n个随机数
      /// </summary>
      /// <param name="n">随机数的个数</param>
      /// <param name="range">随机数产生的范围[0,range)</param>
        /// <param name="seed">随机数种子，默认为0，可以传入系统时间，(int) DateTime.Now.Ticks & 0x0000FFFF</param>
      /// <returns>结果数组，包含n个随机数</returns>
      public static int[] Reservoir(int n, int range, int seed=0)
      {
          if (n>range)//要求产生的个数大于范围，出错
          {
              throw new Exception("要求产生的个数大于范围");
              //return null;
          }
          Random rnd = new Random(seed);
          int[] result = new int[n];
          for (int i = 0; i < n; i++)
          {
              result[i] = i;
          }
          for (int t = n; t < range; t++)
          {
              int j = rnd.Next(0, t + 1);
              if (j<n)
              {
                  result[j] = t;
              }
          }
          return result;
      }

      /// <summary>
      /// 使用ShuffleSelect算法，产生从[0，range）范围的n个随机数. 特点是需要额外的内存空间分配，适合n较小的情况，例如n<1000
      /// </summary>
      /// <param name="n">随机数的个数</param>
      /// <param name="range">随机数产生的范围[0,range)</param>
      /// <param name="seed">随机数种子，默认为0,可以传入系统时间，(int) DateTime.Now.Ticks & 0x0000FFFF</param>
      /// <returns>结果数组，包含n个随机数</returns>
      public static int[] ShuffleSelect(int n, int range, int seed = 0)
      {
          if (n > range)//要求产生的个数大于范围，出错
          {
              throw new Exception("要求产生的个数大于范围");
              //return null;
          }
          Random rnd = new Random(seed);

          //初始化[0-range)范围的数字
          int[] temp   = new int[range];
          for (int i = 0; i < range; i++)
          {
              temp[i] = i;
          }
          //fisher-yates shuffle算法，对上面的数组进行洗牌
          FisherYatesShuffle<int>(temp, true, seed);
         
          //拷贝上面数组的前n个，作为结果输出
          int[] result = new int[n];
          Array.Copy(temp,result,n);
          return result;
      }

      /// <summary>
      /// FisherYates Shuffle算法，对输入数组进行洗牌
      /// </summary>
      /// <typeparam name="T">数组类型</typeparam>
      /// <param name="orgArray">原数组</param>
      /// <param name="isOrgShuffled">是否在原数组上进行洗牌。如果为ture则直接对原数组洗牌，如果为false则产生一个新数组，原数组不变</param>
      /// <param name="seed">随机数种子，默认为0，可以传入系统时间，(int) DateTime.Now.Ticks & 0x0000FFFF</param>
      /// <returns>洗牌后的新数组</returns>
      public static T[] FisherYatesShuffle<T>(T[] orgArray,bool isOrgShuffled, int seed = 0)
      {
          Random rnd = new Random(seed);
          T[] copy;
          if (isOrgShuffled)
          {
              copy = orgArray;
          }
          else
          {
              copy = new T[orgArray.Length];
              Array.Copy(orgArray, copy, copy.Length);
          }
          
          for (int i = 0; i < copy.Length; i++)
          {
              int r = rnd.Next(i, copy.Length);
              T tmp = copy[r];
              copy[r] = copy[i];
              copy[i] = tmp;
          }
          return copy;
      }
    }
}
