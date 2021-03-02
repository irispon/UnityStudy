using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WKoreanSearch
{
    public class WKoreanUitility
    {
        //한글 초성 중성 종성을 분리시켜주는 유틸리티입니다. 원우

        private static char[] _inital = { 'ᄀ', 'ᄁ', 'ᄂ', 'ᄃ', 'ᄄ', 'ᄅ', 'ᄆ', 'ᄇ', 'ᄈ', 'ᄉ', 'ᄊ', 'ᄋ', 'ᄌ', 'ᄍ', 'ᄎ', 'ᄏ', 'ᄐ', 'ᄑ', 'ᄒ' };//초성 문자열
        private static char[] _compable = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };//초성 문자열
        /// <summary>
        /// 초성 수
        /// </summary>
        private const int INITIAL_COUNT = 19;

        /// <summary>
        /// 중성 수
        /// </summary>
        private const int MEDIAL_COUNT = 21;

        /// <summary>
        /// 종성 수
        /// </summary>
        private const int FINAL_COUNT = 28;

        /// <summary>
        /// 한글 유니코드 시작 인덱스
        /// </summary>
        private const int Korean_UNICODE_START_INDEX = 0xac00;

        /// <summary>
        /// 한글 유니코드 종료 인덱스
        /// </summary>
        private const int Korean_UNICODE_END_INDEX = 0xD7A3;



        /// <summary>
        /// 초성 시작 인덱스
        /// </summary>
        private const int INITIAL_START_INDEX = 0x1100;



        /// <summary>
        /// 중성 시작 인덱스
        /// </summary>
        private const int MEDIAL_START_INDEX = 0x1161;



        /// <summary>
        /// 종성 시작 인덱스
        /// </summary>
        private const int FINAL_START_INDEX = 0x11a7;


        /// <summary>
        /// 한글 여부 구하기
        /// </summary>
        /// <param name="source">소스 문자</param>
        /// <returns>한글 여부</returns>
        public static bool IsKorean(char source)
        {

            if (Korean_UNICODE_START_INDEX <= source && source <= Korean_UNICODE_END_INDEX)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 한글 여부 구하기
        /// </summary>
        /// <param name="source">소스 문자열</param>
        /// <returns>한글 여부</returns>

        public static bool IsKorean(string source)
        {
            bool result = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (Korean_UNICODE_START_INDEX <= source[i] && source[i] <= Korean_UNICODE_END_INDEX)
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 한글 초성 중성 종성으로 분리함.
        /// </summary>
        /// <param name="source">소스 한글 문자</param>
        /// <returns>분리된 자소 배열</returns>

        public static char[] DivideKorean(char source)
        {
            char[] elementArray = null;

            if (IsKorean(source))
            {

                int index = source - Korean_UNICODE_START_INDEX;
                int initial = INITIAL_START_INDEX + index / (MEDIAL_COUNT * FINAL_COUNT);
                int medial = MEDIAL_START_INDEX + (index % (MEDIAL_COUNT * FINAL_COUNT)) / FINAL_COUNT;
                int final = FINAL_START_INDEX + index % FINAL_COUNT;

                if (final == 4519)
                {
                    elementArray = new char[2];

                    elementArray[0] = (char)initial;
                    elementArray[1] = (char)medial;

                }
                else
                {

                    elementArray = new char[3];
                    elementArray[0] = (char)initial;
                    elementArray[1] = (char)medial;
                    elementArray[2] = (char)final;

                }
            }
            return elementArray;
        }
        /// <summary>
        /// 각 단어의 초성을 추출함 (가나다라마바사 =>ㄱㄴㄷㄹㅁㅂㅅ)
        /// 일반적인 자음도 초성으로 분류함.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DivideFirstLiteral(string source)
        {
            char[] _elementArray = null;
            source = source.Replace(" ", "");

            int index;

            _elementArray = new char[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                if (!IsConsonant(source[i]))
                {
                    int initial = 0;
                    index = source[i] - Korean_UNICODE_START_INDEX;
                    initial = INITIAL_START_INDEX + index / (MEDIAL_COUNT * FINAL_COUNT);
                    if (initial >= INITIAL_START_INDEX)
                    {
                        // initial = compatible[(initial - 0x1100)];
                    }

                    _elementArray[i] = (char)initial;

                }
                else
                {
                    for (int ch = 0; ch < _inital.Length; ch++)
                    {
                        if (source[i] == _compable[ch])
                            _elementArray[i] = _inital[ch];
                    }


                }



            }


            return new string(_elementArray);
        }
        //호환 언어인지 판단함.
        public static bool IsConsonant(char ch)
        {
            //( 한글자 || 자음 , 모음 )

            if ('ㄱ' <= ch && 'ㅎ' >= ch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 초성 검색, 그냥 단어 검색, 초성+단어 검색을 지원함.
        /// </summary>
        /// <param name="searchedText">검색 받을 이름</param>
        /// <param name="searchText">검색할 이름</param>
        /// <returns></returns>
        public bool Search(string searchedText, string searchText)
        {
            //검색 알고리즘 세분화
            searchText = searchText.Replace(" ", "");//공백 제거
            string _tmpName = searchedText.Replace(" ", "");//공백 제거
            string _tmpSearchText = searchText;
            string _literal = DivideFirstLiteral(_tmpSearchText);//검색할 아이템 이름을 문자로 변경 => 정파 =>ㅈㅍ
            string _itemListeral = DivideFirstLiteral(_tmpName.Replace(" ", ""));//아이템 이름 문자로 변경 =>가나다 =>ㄱㄴㄷ

            int index = -1;

            for (int i = 0; i < searchText.Length; i++)
            {
                if (IsConsonant(searchText[i]))
                {
                    searchText = searchText.Remove(i, 1);
                    i--;
                }
            }//자음 삭제 ex) ㅈ파 => 파

            //1차 자음 검색 ex) ㅈㅍ(정파) 가 들어간 아이템을 1차 필터링
            //이후 자음 검색으로 string을 나눠서 매칭을 한번 더함. ex)(검색어 ㅈ파 => 파) 정풍장풍정파 => 정풍(x), 장풍(x), 정파(o) 

            if (searchText != "")
            {
                for (int i = 0; _itemListeral.IndexOf(_literal) > -1; i++)
                {
                    index = _itemListeral.IndexOf(_literal);
                    if (index < 0)
                        break;

                    if (index > -1 && _tmpName.Substring(index, _tmpSearchText.Length).Contains(searchText))
                    {
                        //Debug.Log("이름 " + name + " 검색 이름 " + searchText);
                        return true;
                    }
                    else
                    {
                        _itemListeral = _itemListeral.Remove(index, _tmpSearchText.Length);
                        _tmpName = _tmpName.Remove(index, _tmpSearchText.Length);
                    }
                }
            }
            else if (_itemListeral.Contains(_literal))//초성만 있고 일치할 때
            {
                return true;
            }


            return false;
        }


    }

}
