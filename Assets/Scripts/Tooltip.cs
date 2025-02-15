﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Text tooltipText;

	void Start () {
        tooltipText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
	}

    public void GenerateTooltip(Item item)
    {
        string tooltip = string.Format("<b>{0}</b>\n{1}\n", item.title, item.description);
        tooltipText.text = tooltip;
        gameObject.SetActive(true);
    }
}
