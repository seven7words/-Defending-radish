using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UICard : MonoBehaviour,IPointerDownHandler
{
    //点击事件
    public event Action<Card> OnClick; 
    public Image ImgCard;
    public Image ImgLock;
    //卡片属性
    Card m_Card = null;
    //是否半透
    bool m_IsTransparent = false;

    public bool IsTransparent
    {
        get { return m_IsTransparent; }
        set
        {
            m_IsTransparent = value;
            Image[] images = new Image[] {ImgCard,ImgLock};
            foreach (Image img in images)
            {
                Color c = img.color;
                c.a = value ? 0.5f : 1f;
                img.color = c;

            }

        }
    }

    public void DataBind(Card card)
    {
        //保存
        m_Card = card;
        //加载图片
        string cardFile = "file://" + Consts.CardDir+"\\" + m_Card.CardImage;
        StartCoroutine(Tools.LoadImage(cardFile, ImgCard));
        //是否透明
        //IsTransparent = card.IsLocked;
        //是否锁定
        ImgLock.gameObject.SetActive(card.IsLocked);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnClick != null)
        {
            OnClick(m_Card);
        }
    }

     void OnDestroy()
    {
        while (OnClick!=null)
        {
            OnClick -= OnClick;
        }
    }

}
