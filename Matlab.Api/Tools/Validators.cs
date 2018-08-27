using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Matlab.Api.Tools;

namespace Matlab.Api
{
    public static class Validators
    {
       

        public static bool MobileValidator(out ResponseMessage responseMessage, ref string mobile, bool requied = false)
        {
            responseMessage = ResponseMessage.Ok;
            if (requied && string.IsNullOrWhiteSpace(mobile))
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.RequierdMobile);
            }
            if (int.TryParse(mobile, out var result))
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.MobileInvalid);
            }
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                mobile = mobile.Trim('+', '0');
                if (mobile.Length == 10)
                {
                    mobile = "+98" + mobile;
                }
                else if (mobile.Length == 12)
                {
                    mobile = "+" + mobile;
                }
                else
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.MobileInvalid);
                }
            }
            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }

        public static bool NationalCodeValidator(out ResponseMessage responseMessage, ref string nationalCode, bool requied = false)
        {
            responseMessage = ResponseMessage.Ok;
            if (requied && string.IsNullOrWhiteSpace(nationalCode))
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.NationalCodeRequied);
            }
            if (!string.IsNullOrWhiteSpace(nationalCode))
            {
                nationalCode = nationalCode.Replace("-", "");
                nationalCode = nationalCode.Replace("_", "");
                nationalCode = nationalCode.Replace(" ", "");
                if (nationalCode.Length != 10)
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.NationalCodeInvalid);
                }
                try
                {
                    char[] chArray = nationalCode.ToCharArray();
                    int[] numArray = new int[chArray.Length];
                    for (int i = 0; i < chArray.Length; i++)
                    {
                        numArray[i] = (int)char.GetNumericValue(chArray[i]);
                    }
                    int num2 = numArray[9];
                    switch (nationalCode)
                    {
                        case "0000000000":
                        case "1111111111":
                        case "22222222222":
                        case "33333333333":
                        case "4444444444":
                        case "5555555555":
                        case "6666666666":
                        case "7777777777":
                        case "8888888888":
                        case "9999999999":
                            return false;
                    }
                    int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                    int num4 = num3 - ((num3 / 11) * 11);
                    if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                    {
                        return true;
                    }
                    else
                    {
                        responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.NationalCodeInvalid);
                    }
                }
                catch
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.NationalCodeInvalid);
                }
            }
            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }

        public static bool PasswordValidator(out ResponseMessage responseMessage, string password, bool replace = false, string oldPassword = null)
        {
            responseMessage = ResponseMessage.Ok;
            if (string.IsNullOrWhiteSpace(password))
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.RequierdPassword);
            }
            else
            {
                if (password.Contains(' '))
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.PasswordInvalidCharacter);
                }
                if (password.Length < 6)
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.PasswordInvalidLenght);
                }
                if (replace && string.IsNullOrWhiteSpace(oldPassword))
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.PasswordInvalidMatch);
                }
            }
            //if (replace && password != oldPassword)
            //{
            //    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.PasswordInvalidMatch);
            //}

            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }

        public static bool StringNullValidator(out ResponseMessage responseMessage, string target, string nullMessage)
        {
            responseMessage = ResponseMessage.Ok;
            if (string.IsNullOrWhiteSpace(target))
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, nullMessage);
            }
            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }

        public static bool ObjectNullValidator(out ResponseMessage responseMessage, object target, string nullMessage)
        {
            responseMessage = ResponseMessage.Ok;
            if (target == null)
            {
                responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, nullMessage);
            }
            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }
       
        public static bool ImageFileValidator(out ResponseMessage responseMessage, ref string propperty, string defaultName,
            HttpFileCollection httpFileCollection, int maxSize = 1048576, string folder = "Images")
        {
            try
            {
                if (httpFileCollection == null || httpFileCollection.Count == 0)
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.ImageNotSent);
                }
                else if (httpFileCollection.Count > 1)
                {
                    responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.ImageMultiUploaded);
                }
                else
                {
                    var postedFile = httpFileCollection[0];
                    if (postedFile.ContentLength == 0)
                    {
                        responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.ImageEmpty);
                    }
                    else
                    {
                        var allowedFileExtensions = new List<string> { ".jpg" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!allowedFileExtensions.Contains(extension))
                        {
                            responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.ImageFileFormat);
                        }
                        else if (postedFile.ContentLength > maxSize)
                        {
                            responseMessage = new ResponseMessage(ResponseMessage.ResponseCode.FormatError, ErrorMessages.ImageFileSize);
                        }
                        else
                        {
                            string fileName = defaultName + extension;

                            var fileAbsPath = HttpContext.Current.Server.MapPath($"~/{folder}/" + fileName);
                            propperty = $"{folder}/{fileName}";
                            postedFile.SaveAs(fileAbsPath);
                            responseMessage = ResponseMessage.Ok;
                        }
                    }
                }
            }
            catch (Exception)
            {
                responseMessage = ResponseMessage.InternalError;
            }
            return responseMessage.Code == ResponseMessage.ResponseCode.Ok;
        }
    }
}