﻿using Assets.Model;
using UnityEngine;
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

    public void Initialize(GameState state)
    {
      GameFieldDrawer.DrawField(FieldContainer, state.Maze);
      var heroes = state.Heroes;

      for (int i = 0; i < heroes.Count; i++)
      {
        var hero = state.Heroes[i];
     
        if (hero.HitPoints > 0)
          GameFieldDrawer.DrawHero(FieldContainer, hero);
      }

      foreach (var chest in state.Chests)
      {
        GameFieldDrawer.DrawChest(FieldContainer, chest);
      }
      SetState(state);
    }

    public void SetState(GameState state)
    {
      _currentState = state;

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
        cardObj.GetComponent<CardListener>().Card = card;
        cardObj.transform.localPosition = Vector3.zero;

        if (i + 1 < cards.Count)
        {
          var sepTemplate = Resources.Load("Prefabs\\Separator") as GameObject;
          var sepObj = Instantiate(sepTemplate, CardHolder);
          sepObj.transform.localPosition = Vector3.zero;
        }
      }
    }
  }
}
