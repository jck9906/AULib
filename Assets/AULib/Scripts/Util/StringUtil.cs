using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{

    public enum eCheckString
    {
        Valid,      // 유효
        Space,      // 공백포함
        Special,    // 특문포함
        Number,     // 숫자포함
        Etc,        // 그외
    }


    public class StringUtil
    {
        public static bool CheckLength( string text , int min , int max )
        {
            if ( min > text.Length || max < text.Length )
                return false;

            return true;
        }

        public static eCheckString CheckNickname( string text )
        {
            if ( IsSpace( text ) )
                return eCheckString.Space;

            if ( IsSpecail( text ) )
                return eCheckString.Special;

            // 특문,공백제외하고는 가능한 문자로 판단해도 될까?
            //foreach ( char ch in text.ToCharArray() )
            //{
            //    if ( !IsEnglish( ch ) && !IsHangle( ch ) && IsNumber( ch ) )
            //        continue;
            //}

            return eCheckString.Valid;
        }

        public static bool IsEnglish( char ch )
        {
            return ( ( ch >= 0x61 && ch <= 0x7A ) || ( ch >= 0x41 && ch <= 0x5A ) ) ? true : false;
        }

        public static bool IsNumber( char ch )
        {
            return ( ch >= 0x03 && ch <= 0x39 ) ? true : false;
        }

        public static bool IsHangle( char ch )
        {
            return ( ( ch >= 0xAC00 && ch <= 0xD7A3 ) || ( ch >= 0x3131 && ch <= 0x318E ) ) ? true : false;
        }

        public static bool IsSpecail( string text )
        {
            // string strPattern = @"[!@#$%^&*()_+=\[{\]};:<>|./?,- ]";
            string strPattern = @"[!@#$%^&*()+=\[{\]};:<>|/?,-]";

            System.Text.RegularExpressions.Regex regex = null;
            regex = new System.Text.RegularExpressions.Regex( strPattern );

            return regex.IsMatch( text );
        }

        public static bool IsSpace( string text )
        {
            if ( -1 != text.IndexOf( ' ' ) )
                return true;

            return false;
        }

        public static Color StringToColor( string str )
        {
            Color colorTemp;
            ColorUtility.TryParseHtmlString( string.Format( "#{0}" , str ) , out colorTemp );

            return colorTemp;
        }

        public static Vector2 StringToVector2( string str )
        {
            string[] strSplit = str.Split( '|' );
            if ( strSplit.Length != 2 )
                return Vector2.zero;

            return new Vector2( float.Parse( strSplit[ 0 ] ) , float.Parse( strSplit[ 1 ] ) );
        }


        public static Vector3 StringToVector3( string str )
        {
            string[] strSplit = str.Split( '|' );
            if ( strSplit.Length != 3 )
                return Vector3.zero;

            return new Vector3( float.Parse( strSplit[ 0 ] ) , float.Parse( strSplit[ 1 ] ) , float.Parse( strSplit[ 2 ] ) );
        }


        public static Vector4 StringToVector4( string str )
        {
            string[] strSplit = str.Split( '|' );
            if ( strSplit.Length != 4 )
                return Vector4.zero;

            return new Vector4( float.Parse( strSplit[ 0 ] ) , float.Parse( strSplit[ 1 ] ) , float.Parse( strSplit[ 2 ] ) , float.Parse( strSplit[ 3 ] ) );
        }

        public static Quaternion StringToQuaternion( string str )
        {
            string[] strSplit = str.Split( '|' );
            if ( strSplit.Length != 4 )
                return Quaternion.identity;

            return new Quaternion( float.Parse( strSplit[ 0 ] ) , float.Parse( strSplit[ 1 ] ) , float.Parse( strSplit[ 2 ] ) , float.Parse( strSplit[ 3 ] ) );
        }

        public static string ColorToString( Color color )
        {
            return "";
        }

        // Enum에 string에 해당하는 값이 존재하는가?
        public static bool IsEnumDefined<T>( string strValue )
        {
            return Enum.IsDefined( typeof( T ) , strValue );
        }

        // string을 enum value로 casting
        public static T ParseEnum<T>( string strValue )
        {
            object obj = Enum.Parse( typeof( T ) , strValue , true );
            return ( T ) obj;
        }

        public static string CommaText( int num )
        {
            if ( num == 0 )
                return num.ToString();
            return string.Format( "{0:#,###}" , num );
        }

        public static string TitleCaseString( string s )
        {
            if ( s == null )
                return null;

            string[] words = s.ToLower().Split( ' ' );
            for( int i = 0 ; i < words.Length ; i++  )
            {
                if ( words[ i ].Length == 0 )
                    continue;

                Char firstChar = Char.ToUpper( words[ i ][ 0 ] );
                string rest = "";
                if ( words[ i ].Length > 1 )
                    rest = words[ i ].Substring( 1 ).ToLower();
                words[ i ] = firstChar + rest;
            }

            return string.Join( " " , words );
        }


        //////////////////////////////////
        ///
        string SecondsToFamiliarText( ulong seconds , bool useZeroSec )
        {
            ulong d = seconds / 86400; if( d > 0 ) seconds -= ( d * 86400 );
            ulong h = seconds / 3600;  if( h > 0 ) seconds -= ( h * 3600 );
            ulong m = seconds / 60;    if( m > 0 ) seconds -= ( m * 60 );
            ulong s = seconds % 60;

            string rv = string.Empty;

            if( d != 0 )
            {
                if( !string.IsNullOrEmpty( rv ) )
                    rv += " ";
                rv += $"{d}일";
            }

            if ( h != 0 )
            {
                if ( !string.IsNullOrEmpty( rv ) )
                    rv += " ";
                rv += $"{h}시";
            }

            if ( m != 0 )
            {
                if ( !string.IsNullOrEmpty( rv ) )
                    rv += " ";
                rv += $"{m}분";
            }

            if ( s != 0 )
            {
                if ( !string.IsNullOrEmpty( rv ) )
                    rv += " ";
                rv += $"{s}초";
            }


            return rv;
        }
    }
}