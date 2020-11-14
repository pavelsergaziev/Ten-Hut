//TODO:
//+ разбить на LanguageController и LanguageDependentTextsController
//+ сделать абстрактный класс для монобехэвиоров с выводом текста
//+ перекинуть кнопки в buttons enum и сделать соотв. дописки в соотв. скрипты
//+ сделать всех наследников абстр. класса монобехэвиоров для вывода текста и посмотреть, какие пункты в текстах ещё нужны
//+ прописать json-ы с русскими и англ. текстами (подумать над русским переводом. Потому что "На-а-а-Верх! Лево! На!" выглядит тупо.)                                                                 
//+ сделать сохранение ранее выбранного языка. (запомнить, что playerPrefs лучше вообще не использовать, даже для таких вещей, потому что на многих девайсах данные в PlayerPrefs может обнулить даже банальная очистка кэша. А если онлайн, читеры, лидерборды или купленные за деньги вещи, то тут ещё и абсолютная незащищённость данных. Сохранение данных походу делается через сериализацию файлов.)
//+ перекинуть в дикшнари.
//+ разобраться со шрифтами.
//+ поправить цвет текста.


//походу все манипуляции с текстом и шрифтами стоит разделить на:
//1.собственно текст (зависит от языка).
//2.опции формата вывода, зависящие от языка (шрифт, м.б. размер).
//3.опции формата текста, независящие от языка (цвет).
//И их можно будет в монобехах подцеплять в разное время, чтобы не ловить каждый раз в ивенте текст+фонт+размер.

using System;
using UnityEngine;

namespace LanguagesAndTexts
{

    public class LanguageController : IDependencyInjectionReceiver
    {
        public event Action<Languages> OnLanguageChanged = delegate { };

        private LanguageDependentTextsController _textsController;
        private UIButtonEffectsController _buttonsController;
        private OptionsInitialStateController _optionsSaver;

        private Languages _currentLanguage;
        private int _languagesCount;

        public LanguageController()
        {
            Main.Instance.SubscribeToDependencyInjection(this);
            _languagesCount = Enum.GetValues(typeof(Languages)).Length;
        }

        public void SetLanguageOnGameStart()
        {
            ChangeLanguage(_optionsSaver.GetInitialLanguage());
        }

        public void InjectDependencies()
        {
            _textsController = Main.Instance.LanguageDependentTextsController;
            _optionsSaver = Main.Instance.OptionsInitialStateController;
            _buttonsController = Main.Instance.UIButtonEffectsController;

            _buttonsController.OnButtonPressed += ChangeLanguageOnButtonPress;
        }

        private void ChangeLanguageOnButtonPress(MenuButtons buttonPressed)
        {
            if (buttonPressed == MenuButtons.ChangeLanguageToNext)
            {
                ChangeLanguageToNext();
                return;
            }

            if (buttonPressed == MenuButtons.ChangeLanguageToPrev)
            {
                ChangeLanguageToPrev();
                return;
            }
        }


        public void ChangeLanguageToNext()
        {
            _currentLanguage++;
            if ((int)_currentLanguage == _languagesCount)
            {
                _currentLanguage = 0;
            }

            SetLanguage(_currentLanguage);
        }

        public void ChangeLanguageToPrev()
        {
            _currentLanguage--;
            if (_currentLanguage < 0)
            {
                _currentLanguage = (Languages)(_languagesCount - 1);
            }

            SetLanguage(_currentLanguage);
        }

        public void ChangeLanguage(Languages targetLanguage)
        {
            _currentLanguage = targetLanguage;

            SetLanguage(targetLanguage);
        }

        private void SetLanguage(Languages targetLanguage)
        {
            _textsController.LoadTextsInLanguage(_currentLanguage);
            OnLanguageChanged.Invoke(_currentLanguage);

            _optionsSaver.SetInitialLanguage(_currentLanguage);
        }

        private void OnDestroy()
        {
            _buttonsController.OnButtonPressed -= ChangeLanguageOnButtonPress;
        }

        //public void ChangeLanguage(Languages targetLanguage)
        //{

        //    Texts = JsonUtility.FromJson<Texts>(Main.Instance.Settings.LanguagesSettings._dictionary[targetLanguage].text);

        //    OnTextsChanged.Invoke(Texts);
        //    _currentLanguage = targetLanguage;
        //}
    }

}