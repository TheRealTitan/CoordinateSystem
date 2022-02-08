using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using org.mariuszgromada.math.mxparser;
using System.IO;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
	[SerializeField] GameObject graphPrefab;
	[SerializeField] GameObject targetPrefab;
	
	public static string currentPartialExpression = "";

	private static float xInc = 0.007f;
	private static int stageNum = 0;
	private static string currentExpression = "0";
	private static Canvas canvas;
	private static List<Stage> stages = new List<Stage>();
	private static float startTime;
	private static bool inPlay;
		
	// Start is called before the first frame update
	void Start()
	{
		startTime = Time.time;
		inPlay = false;

		//Find the canvas
		GameObject ui = GameObject.FindGameObjectWithTag("UI - Toggleable");
		canvas = ui.GetComponent<Canvas>();

		stages = CreateStages();

		foreach (Stage s in stages)
		{
			Debug.Log(s.PartialExpression);
		}

		NextStage();
	}

	private void NextStage()
	{
		inPlay = false;

		//Reset stage
		CleanUpStage();

		if (stageNum < stages.Count)
		{
			//Start a new game
			StartStage();

			//Increment stage
			stageNum++;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (inPlay && Time.time - startTime > 6)
		{
			if (NoMoreTargets())
			{
				//Debug.Log("No more targets");
				NextStage();
			}
			else
			{
				ResetStage();
			}
		}
	}

	public void Go()
	{
		startTime = Time.time;
		inPlay = true;

		HideUI();

		CreateGraphObject(currentExpression, 0, xInc);
	}

	private void CreateGraphObject(string function, float startingX, float xInc)
	{
		GameObject graph = Instantiate(graphPrefab);
		Mover mover1 = graph.GetComponent<Mover>();

		mover1.stringFunction = FixExpressionString(function);
		mover1.x = startingX;
		mover1.xInc = xInc;
	}

	private void CreateTargetObject(int xCoord, int yCoord)
	{
		GameObject target = Instantiate(targetPrefab);
		target.transform.position = new Vector3(xCoord, yCoord, 0);
	}

	private void CleanUpStage()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
		GameObject[] graphs = GameObject.FindGameObjectsWithTag("Player");

		foreach (GameObject t in targets)
		{
			GameObject.Destroy(t);
		}

		foreach (GameObject g in graphs)
		{
			GameObject.Destroy(g);
		}
	}

	private void StartStage()
	{
		Stage stage = stages[stageNum];

		foreach(TargetCoord target in stage.TargetCoords)
		{
			CreateTargetObject(target.X, target.Y);
		}

		//Check for player --> Spawn it.
		GameObject playerBase = GameObject.Find("PlayerBase");

		if (playerBase == null)
		{
			Debug.Log("PlayerBase not there");
		}

		//Enable UI
		ShowUI();
		currentPartialExpression = stage.PartialExpression;
		GameObject.Find("Expression").GetComponent<InputField>().text = "";
		ChangeExpression("");
	}

	private bool NoMoreTargets()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

		if (targets.Length == 0)
		{
			return true;
		}

		return false;
	}

	public static void HideUI()
	{
		if (canvas != null)
		{
			canvas.enabled = false;
			canvas.gameObject.SetActive(false);
		}
	}

	public static void ShowUI()
	{
		if (canvas != null)
		{
			canvas.enabled = true;
			canvas.gameObject.SetActive(true);
		}
	}

	public static void ChangeExpression(string expression)
	{
		currentExpression = FixExpressionString(expression) + FixExpressionString(currentPartialExpression);
		Debug.Log("Expression changed to: " + expression);
		
	}

	public static string GetExpression()
	{
		return currentExpression;
	}

	private static string FixExpressionString(string exp)
	{
		exp = exp.ToLower();
		string newExp = string.Empty;
		for (int i = 0; i < exp.Length; i++)
		{
			newExp += exp[i];
			if (i < exp.Length - 1 && ((exp[i] == 'x' && char.IsDigit(exp[i + 1])) || (exp[i + 1] == 'x' && char.IsDigit(exp[i]))))
			{
				newExp += '*';
			}
		}

		return newExp;
	}

	//Don't destroy object when changing scene
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void ResetStage()
	{
		stageNum--;

		NextStage();
	}

	private static List<Stage> CreateStages()
	{
		List<Stage> stages = new List<Stage>();

		Stage stage1 = new Stage()
		{
			PartialExpression = "",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 2
				}
			}
		};
		stages.Add(stage1);

		Stage stage2 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 3,
					Y = 3
				}
			}
		};
		stages.Add(stage2);

		Stage stage3 = new Stage()
		{
			PartialExpression = "",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 4
				}
			}
		};
		stages.Add(stage3);

		Stage stage4 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 4
				}
			}
		};
		stages.Add(stage4);

		Stage stage5 = new Stage()
		{
			PartialExpression = "+4",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 1,
					Y = 2
				}
			}
		};
		stages.Add(stage5);

		Stage stage6 = new Stage()
		{
			PartialExpression = "",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 1,
					Y = 2
				},
				new TargetCoord
				{
					X = 3,
					Y = 2
				}
			}
		};
		stages.Add(stage6);

		Stage stage7 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 2
				},
				new TargetCoord
				{
					X = 4,
					Y = 2
				}
			}
		};
		stages.Add(stage7);

		Stage stage8 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 2
				},
				new TargetCoord
				{
					X = 4,
					Y = 2
				}
			}
		};
		stages.Add(stage8);

		Stage stage9 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 2
				},
				new TargetCoord
				{
					X = 4,
					Y = 2
				}
			}
		};
		stages.Add(stage9);

		Stage stage10 = new Stage()
		{
			PartialExpression = "+2",
			TargetCoords = new List<TargetCoord>()
			{
				new TargetCoord
				{
					X = 2,
					Y = 2
				},
				new TargetCoord
				{
					X = 4,
					Y = 2
				}
			}
		};
		stages.Add(stage10);

		return stages;
	}
}
