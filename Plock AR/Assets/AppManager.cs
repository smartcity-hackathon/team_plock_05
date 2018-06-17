
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct CurrentLevelState
{
/// <summary>
/// Never change it. It is just for test.
/// </summary>
    public ScreenOrient screenOrient;
    public BasicScreen basicScreen;
    public FurTranslationState furTranslationState;
    public FurMatState furMatState;
    public GuiFilters guiFilters;
    public SecondPanels secondPanels;
};
/// <summary>
/// Never change it. It is just for test.
/// </summary>
public enum ScreenOrient { IGNORE = 0, Vertical = 1, Horizontal = 5 };
public enum BasicScreen { IGNORE = 0, basicScreen = 1, startingScreen = 2, furnitureEditGUI = 3, LibraryGUIbasic = 6, furInsertionGui = 7, MatChangeGui = 8, Settings = 9 };
public enum FurTranslationState { IGNORE = 0, None = 1, Translate = 2, Rotate = 3};
public enum FurMatState { IGNORE = 0, Furniture = 1, Materials = 2, Finishing = 3 };
public enum GuiFilters { IGNORE = 0, AZ = 1, Favorite = 2, Collection = 3, Type = 4, Search = 5 };
public enum SecondPanels { IGNORE = 0, None = 1, CollectionPanel = 2, TypePanel = 3, SearchPanel = 4, Galery = 5 };

public class AppManager : MonoBehaviour {

    /*
     Logika trybów

x1.x2.x3.x4.x5.x6 + 

x1 : (informacje n.t. ukladu ekranu) : 1 - pionowe, 5 - poziome (rozwazyc mozżna więcej wariantów)
x2 : (informacje n.t. GUI podstawowego) : 1 - Gui schowane:podstawowe, 2 - StartingScreen 3 - edycja mebla, 6 - Gui meblowomaterialowe, 7 - Proba wstawiania, 8 - Gui ustawieniowe 
x3 : (manipulacja mebla) : 1 nie jest manipulowany, 2 - jest przesuwany, 3 - jest obracany
x4 : (Meble czy matetialy) : 1 - meble, 2 materialy, 3 - dodatki 
x5 : (Rodzaj Wybóru mebli) : 1 - AZ, 2 - ulubione, 3 - kolecje, 4 - Typy, 5 - wyszukiwanie
x6 : (Dodatkowe Gui) : 1 - dodatkowe Gui schowane, 2 - panel kolekcji, 3 - panel typów, 4 - panel wyszukiwania, 5 - Poszerzony wybor mebla

x7 : Dalsze dane na temat pobrania mebli itp.

    */






    public static CurrentLevelState currentLevelState;
    private CurrentLevelState lastLevelState;

    //Animation Manager reference
    public AnimateUiPanel animationManager;

    public TextMeshProUGUI DebugText;


    //Asset Button
    public GameObject chooseItem;
    public GameObject acceptItem;

    //Edit Asset Buttons

    public GameObject deleteItem;
    public GameObject editItem;

    //Panels:

    //main menu panel
    public UiPanel mainMenu;
    public UiPanel mainMenuButton; // should be invisible when in Welcome Area


    //camera view panels
    public UiPanel welcomeArea;
    public GameObject ARArea;

    //selection area panels
    public UiPanel mainArea;
    public UiPanel searchBar;

    public UiPanel collectionFilterBar;
    public UiPanel typeFilterBar;

    //asset detail popup area
    public GameObject popupArea;

    //blur panel for applying blur effect to background
    public GameObject blurPanel;

    private List<UiPanel> AnimatedElements;

    private Vector2 resolution;

    private bool startedWithClick;
    private void Awake()
    {
        currentLevelState.screenOrient = ScreenOrient.Vertical;// bramka logiczna
        currentLevelState.basicScreen = BasicScreen.basicScreen;
        currentLevelState.furTranslationState = FurTranslationState.None;
        currentLevelState.furMatState = FurMatState.Furniture;
        currentLevelState.guiFilters = GuiFilters.AZ;
        currentLevelState.secondPanels = SecondPanels.None;

        startedWithClick = false;
        resolution = new Vector2(Screen.width, Screen.height);
        AnimatedElements = new List<UiPanel>() { mainArea, mainMenu };
    }
    private void AstartWithClick()
    {
        // mainMenuButton//goes up
        animationManager.FadePanel(mainMenuButton);
        // welcomeArea//goes down
        animationManager.FadePanel(welcomeArea);
    }

    public void changeState(TargetState tState)
    {
        //tState.MyTargetState.screenOrient;
        if (tState.MyTargetState.basicScreen>0)
            currentLevelState.basicScreen = tState.MyTargetState.basicScreen;
        if (tState.MyTargetState.furTranslationState > 0)
            currentLevelState.furTranslationState = tState.MyTargetState.furTranslationState;
        if (tState.MyTargetState.furMatState > 0)
            currentLevelState.furMatState = tState.MyTargetState.furMatState;
        if (tState.MyTargetState.guiFilters > 0)
            currentLevelState.guiFilters = tState.MyTargetState.guiFilters;
        if (tState.MyTargetState.secondPanels > 0)
            currentLevelState.secondPanels = tState.MyTargetState.secondPanels;
    }

    void Update()
    {
        DebugText.SetText((1.0f / Time.deltaTime).ToString());
        return;
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            // do your stuff
            Debug.Log("IGO");

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
        if (!startedWithClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("lolison");
                AstartWithClick();
                startedWithClick = true;
            }
        }
        if (lastLevelState.screenOrient != currentLevelState.screenOrient 
            || lastLevelState.basicScreen != currentLevelState.basicScreen 
            || lastLevelState.furTranslationState != currentLevelState.furTranslationState 
            || lastLevelState.furMatState != currentLevelState.furMatState 
            || lastLevelState.guiFilters != currentLevelState.guiFilters 
            || lastLevelState.secondPanels != currentLevelState.secondPanels)
        {
            // wszystko w tym bloku
            Debug.Log("Myspace");
            DebugText.SetText(currentLevelState.screenOrient.ToString() + ">>" 
                +currentLevelState.basicScreen.ToString() + ">>" 
                + currentLevelState.furTranslationState.ToString() + ">>" 
                + currentLevelState.furMatState.ToString() + ">>"
                + currentLevelState.guiFilters.ToString() + ">>"
                + currentLevelState.secondPanels.ToString());
            lastLevelState = currentLevelState;

        }

    }
}
