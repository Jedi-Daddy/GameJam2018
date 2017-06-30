using System.Text;
using Assets.Model;
using Assets.Model.Cards;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.ViewModel
{
  public class GameViewManager : MonoBehaviour
  {
    private GameState _currentState;
    
    public RectTransform FieldContainer;

    public HeroInfoView CurrentHeroInfo;
    public HeroInfoView[] HeroesInfo;
    public RectTransform CardHolder;
    public Text TimerText;
    public Text GameLogText;

    private StringBuilder _messageSb;

    public void Initialize(GameState state)
    {
      _messageSb = new StringBuilder();
      GameFieldDrawer.DrawField(FieldContainer, state.Maze);
      var heroes = state.Heroes;

      foreach (var chest in state.Chests)
      {
        GameFieldDrawer.DrawChest(FieldContainer, chest);
      }

      for (int i = 0; i < heroes.Count; i++)
      {
        var hero = state.Heroes[i];
     
        if (hero.HitPoints > 0)
          GameFieldDrawer.DrawHero(FieldContainer, hero);
      }
      
      SetState(state);
    }

    public void SetState(GameState state)
    {
      var message = state.Message;
      state.Message = null;
      if(!string.IsNullOrEmpty(message))
        PrintMessage(message);
      if(state.IsWin)
        SceneManager.LoadScene(0);

      if (state.SegmentToRebuild.HasValue)
      {
        if (FieldContainer.childCount > 0)
          for (var i = 0; i < FieldContainer.childCount; i++)
            Destroy(FieldContainer.GetChild(i).gameObject);
        GameFieldDrawer.DrawField(FieldContainer, state.Maze);
        
        foreach (var chest in state.Chests)
        {
          GameFieldDrawer.DrawChest(FieldContainer, chest);
        }

        for (int i = 0; i <  state.Heroes.Count; i++)
        {
          var hero = state.Heroes[i];

          if (hero.HitPoints > 0)
            GameFieldDrawer.DrawHero(FieldContainer, hero);
        }
        
        state.SegmentToRebuild = null;
      }

      var children = FieldContainer.GetComponentsInChildren<Passible>();
      foreach (var child in children)
      {
        Destroy(child.gameObject);
      }

      foreach (var passibleCell in state.Path)
      {
        var cell = Instantiate(Resources.Load("Prefabs\\passableCell")) as GameObject;
        cell.transform.SetParent(FieldContainer.transform);
        cell.transform.localPosition = CoordsUtility.GetUiPosition(passibleCell.Cell);
      }

      CurrentHeroInfo.UpdateHero(state.MaxHitPoints, state.CurrentPlayer, state.CurrentHero);
      var heroes = state.Heroes;
      for (int i = 0, j = 0; i < heroes.Count;i++)
      {
        var hero = state.Heroes[i];
        if (hero != state.CurrentHero)
        {
          HeroesInfo[j].UpdateHero(state.MaxHitPoints, state.CurrentPlayer, hero);
          j++;
        }
      }

      //var cardButtons = CardHolder.GetComponentInChildren<CardListener>();
      if (CardHolder.childCount > 0)
        for (var i = 0; i < CardHolder.childCount; i++)
          Destroy(CardHolder.GetChild(i).gameObject);

      var cards = state.CurrentPlayer.Cards;

      for (var i = 0; i < cards.Count; i++)
      {
        var card = cards[i];
        var cardTemplate = Resources.Load(card.Type == CardType.Attack ? "Prefabs\\attackCardPrefab" : "Prefabs\\defenceCard") as GameObject;
        var cardObj = Instantiate(cardTemplate, CardHolder);
        var cardListener = cardObj.GetComponent<CardListener>();
        cardListener.Card = card;
        cardListener.SetDescription();
        cardObj.transform.localPosition = Vector3.zero;

        if (i + 1 < cards.Count)
        {
          var sepTemplate = Resources.Load("Prefabs\\Separator") as GameObject;
          var sepObj = Instantiate(sepTemplate, CardHolder);
          sepObj.transform.localPosition = Vector3.zero;
        }
      }
    }

    public void PrintMessage(string message)
    {
      if (GameLogText == null || _messageSb == null)
        return;

      _messageSb.Insert(0, "<color=#008000ff> > </color>" + message + "\n");

      GameLogText.text = _messageSb.ToString();
    }
  }
}
