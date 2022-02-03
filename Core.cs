using System;

namespace A2B
{
    public class Core
    {
        public static string AtoB(string text, string[] a, string[] b, int index = 0, int length = 1)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            if (index < 0)
            {
                index = 0;
            }

            if (length < 1)
            {
                length = 1;
            }

            if (a.Length != b.Length)
            {
                throw new ArgumentException("a size : " + a.Length.ToString() + "\r\n"
                                                + "b size : " + b.Length.ToString());
            }
            else if (text.Length < index + length)
            {
                throw new ArgumentException("{"+ text + "}: " + text.Length.ToString() + "\r\n"
                                                + "index : " + index.ToString() + "\r\n"
                                                + "length : " + length.ToString());
            }

            bool isLook = false;
            string target = text.Substring(index, length);
            string convert = target;
            for (int i = 0; i < a.Length; i++)
            {
                if (target == a[i])
                {
                    isLook = true;
                    convert = b[i];
                    break;
                }
            }

            if (isLook)
            {
                if (text.Length > index + length)
                {
                    convert += AtoB(text, a, b, index + length);
                }
            }
            else
            {
                if (text.Length >= index + length + 1)
                {
                    convert = AtoB(text, a, b, index, length + 1);
                }
                else if (text.Length > index + 1)
                {
                    convert = text[index] + AtoB(text, a, b, index + 1);
                }
            }

            return convert;
        }

        public static string AtoBRev(string text, string[] a, string[] b, int index = 0, int length = -1)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            if (index < 0)
            {
                index = 0;
            }

            if (length < 1)
            {
                length = text.Length - index;
            }

            if (a.Length != b.Length)
            {
                throw new ArgumentException("a size : " + a.Length.ToString() + "\r\n"
                                                + "b size : " + b.Length.ToString());
            }
            else if (text.Length < index + length)
            {
                throw new ArgumentException("{"+ text + "}: " + text.Length.ToString() + "\r\n"
                                                + "index : " + index.ToString() + "\r\n"
                                                + "length : " + length.ToString());
            }

            bool isLook = false;
            string target = text.Substring(index, length);
            string convert = target;
            for (int i = 0; i < a.Length; i++)
            {
                if (target == a[i])
                {
                    isLook = true;
                    convert = b[i];
                    break;
                }
            }

            if (isLook)
            {
                if (text.Length > length)
                {
                    convert += AtoBRev(text, a, b, index + length);
                }
            }
            else
            {
                if (length > 1)
                {
                    convert = AtoBRev(text, a, b, index, length - 1);
                }
                else if (text.Length > index + 1)
                {
                    convert = text[index] + AtoBRev(text, a, b, index + 1);
                }
            }

            return convert;
        }
    }
}
