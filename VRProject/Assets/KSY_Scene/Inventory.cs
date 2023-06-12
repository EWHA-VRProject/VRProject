using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    public Transform slotParent;
    [SerializeField]
    public Slot[] slots;
    public Item SelectedItem;
    public List<Renderer> renderers;
    public Renderer SelectedItemRenderer;
    public void OnValidate() {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }


    public void Awake() {
        FreshSlot();
        
    }

    public void FreshSlot() {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++) {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++) {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item, Renderer itemRenderer) {
        if (items.Count < slots.Length) {
            items.Add(_item);
            renderers.Add(itemRenderer);
            FreshSlot();
        } else {
            print("슬롯이 가득 차 있습니다.");
        }
    }
    public void ClickSlot1(){
        Item _item=items[0];
        Renderer renderer=renderers[0];
        if(_item){
            items.RemoveAt(0);
            renderers.RemoveAt(0);
            FreshSlot();
            SelectedItem= _item;
            SelectedItemRenderer=renderer;
            //클릭한 타겟 컴포넌트로 받아서 거기에 아이템 적용
        }
    }
    public void ClickSlot2(){
        Item _item=items[1];
        Renderer renderer=renderers[1];
        if(_item){
            items.RemoveAt(1);
            renderers.RemoveAt(1);
            FreshSlot();
            SelectedItem= _item;
            SelectedItemRenderer=renderer;
        }
    }
    public void ClickSlot3(){
        Item _item=items[2];
        Renderer renderer=renderers[2];
        if(_item){
            items.RemoveAt(2);
            renderers.RemoveAt(2);
            FreshSlot();
            SelectedItem= _item;
            SelectedItemRenderer=renderer;
        }
    }
    public void ClickSlot4(){
        Item _item=items[3];
        Renderer renderer=renderers[3];
        if(_item){
            items.RemoveAt(3);
            renderers.RemoveAt(3);
            FreshSlot();
            SelectedItem= _item;
            SelectedItemRenderer=renderer;
        }
    }
    public void ClickSlot5(){
        Item _item=items[4];
        Renderer renderer=renderers[4];
        if(_item){
            items.RemoveAt(4);
            renderers.RemoveAt(4);
            FreshSlot();
            SelectedItem= _item;
            SelectedItemRenderer=renderer;
        }
    }

    public void DeleteAll(){
        items.Clear();
        renderers.Clear();
        for (int i=0; i < slots.Length; i++) {
            slots[i].item = null;
        }
    }


}