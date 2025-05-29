using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EventTrigger : MonoBehaviour, IPointerClickHandler
{
    public CardFarmingManager CardFarmingManager;
    private RectTransform cardRectTransform;
    private Transform frameArea;
    private Transform handArea;
    private Button cardButton;

    void Start()
    {
        cardRectTransform = GetComponent<RectTransform>();

        GameObject frameObj = GameObject.Find("FrameArea");
        if (frameObj != null)
        {
            frameArea = frameObj.transform;
        }
        else
        {
            Debug.LogError("❌ FrameArea ไม่พบใน Scene! ตรวจสอบว่าได้ตั้งชื่อถูกต้องหรือยัง");
        }

        handArea = transform.parent;
        if (handArea == null)
        {
            Debug.LogError("❌ handArea เป็น null! ตรวจสอบว่า GameObject นี้มี Parent หรือไม่");
        }

        cardButton = GetComponent<Button>();
        if (cardButton != null)
        {
           cardButton.onClick.AddListener(MoveCardToFrame);
        }
        else
        {
            Debug.LogError("❌ Button component ไม่พบใน " + gameObject.name);
        }

        if (CardFarmingManager == null)
        {
            Debug.LogError("❌ CardFarmingManager ไม่ถูกกำหนดใน Inspector!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardFarmingManager != null)
        {
            CardFarmingManager.MoveCardToFrame(gameObject);
        }
        else
        {
            Debug.LogError("❌ cardFarmingManager เป็น null! ตรวจสอบว่าได้กำหนดค่าใน Inspector หรือไม่");
        }
    }
    
    private void MoveCardToFrame()
    {
        foreach (Transform frame in frameArea)
        {
            foreach (Transform slot in frame)
            {
                if (slot.childCount == 0)
                {
                    transform.SetParent(slot);
                    transform.localPosition = Vector3.zero;
                    Debug.Log($"✅ ย้าย {gameObject.name} ไปที่ {slot.name}");
                    return;
                }
            }
        }

        Debug.Log("❌ FrameArea เต็มแล้ว!");
    }
    
    public void ReturnToHand()
    {
        if (handArea != null)
        {
            transform.SetParent(handArea);
            transform.localPosition = Vector3.zero;
            Debug.Log($"🔄 {gameObject.name} กลับไปที่ HandArea");
        }
        else
        {
            Debug.LogError("❌ handArea เป็น null! ตรวจสอบว่า GameObject มี Parent หรือไม่");
        }
    }



}
