using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 20f;
    public float gravity = -1.81f;
    public Camera cam;

    Vector3 velocity;
    public TextMeshProUGUI itrIcon, unlockedAction, objective;
    public Text progress, errorList;
    public InputField inCode, txtFeedback;
    public List<CodeableObject> codeObjects = new List<CodeableObject>();
    public GameObject ground;
    CodeableObject currentObj;
    bool typing = false;

    public enum UIstate {pointer, objectFound, codeWindow};
    UIstate currentUI = UIstate.pointer;

    private void Start()
    {
        errorList.text = "";
        objective.enabled = true;
        objective.text = $"Find an interactive object";

        unlockedAction.enabled = false;
        currentUI = UIstate.pointer;

        UIupdate();
        populateList();
    }

    public void populateList()
    {
        codeObjects[3].code += "<color=#5C83CD>public class</color><color=#49B494> CubeZ</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t}\n}";
        codeObjects[4].code += "<color=#5C83CD>public class</color><color=#49B494> Drone v2</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t}\n}";
        codeObjects[0].code += "<color=#5C83CD>public class</color><color=#49B494> CubeX</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#49B494>Renderer </color> objRenderer;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tobjRenderer.material.color = <color=#BDDCAF>Color</color>.red;\n\t\t}\n}";
        
        codeObjects[1].code += "<color=#5C83CD>public class</color><color=#49B494> CubeY</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#49B494>Renderer </color> objRenderer;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t\t\tobjRenderer.material.color = <color=#BDDCAF>Color</color>.red;\n\t\t}\n}";
        
        //modify rotation but keep color same (given code to change both)
        codeObjects[5].code += "<color=#5C83CD>public class</color><color=#49B494> Tiny Cube</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#49B494>Renderer </color> objRenderer;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t\t\tobjRenderer.material.color = <color=#BDDCAF>Color</color>.red;\n\t\t}\n}";

        //syntax error
        codeObjects[6].code += "<color=#5C83CD>public class</color><color=#49B494> Rectangle</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#49B494>Renderer </color> objRenderer;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t\t\tobjRenderer.material.color = <color=#BDDCAF>Color</color>.red;\n\t\t}\n}";
        
        //blank object
        codeObjects[7].code += "<color=#5C83CD>public class</color><color=#49B494> Prism</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#49B494>Renderer </color> objRenderer;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Modify</color>( )\n\t\t{\n\t\t\t\tthis.transform.<color=#F0DB90>Rotate</color>(0f, 0f,30f, <color=#BDDCAF>Space</color>.Self);\n\t\t\t\tobjRenderer.material.color = <color=#BDDCAF>Color</color>.red;\n\t\t}\n}";

        //look at and follow
        codeObjects[2].code += "<color=#5C83CD>public class</color><color=#49B494> Drone</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#5C83CD>public </color><color=#49B494>Transform</color> player;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Follow</color>( )\n\t\t{\n\t\t\t\ttransform.<color=#F0DB90>LookAt</color>(player);\n\t\t}\n}";

        codeObjects[1].position = codeObjects[1]._gameObject.transform.position;
        codeObjects[2].position = codeObjects[2]._gameObject.transform.position;
        codeObjects[3].position = codeObjects[3]._gameObject.transform.position;
        codeObjects[4].position = codeObjects[4]._gameObject.transform.position;
        codeObjects[5].position = codeObjects[5]._gameObject.transform.position;
        codeObjects[6].position = codeObjects[6]._gameObject.transform.position;
        codeObjects[7].position = codeObjects[7]._gameObject.transform.position;

        codeObjects[0].rotation = codeObjects[0]._gameObject.transform.rotation;
        codeObjects[1].rotation = codeObjects[1]._gameObject.transform.rotation;
        codeObjects[2].rotation = codeObjects[2]._gameObject.transform.rotation;
        codeObjects[3].rotation = codeObjects[3]._gameObject.transform.rotation;
        codeObjects[4].rotation = codeObjects[4]._gameObject.transform.rotation;
        codeObjects[5].rotation = codeObjects[5]._gameObject.transform.rotation;
        codeObjects[6].rotation = codeObjects[6]._gameObject.transform.rotation;
        codeObjects[7].rotation = codeObjects[7]._gameObject.transform.rotation;

        codeObjects[0].objective = $"Set <color=#EAD565>{codeObjects[0].name}</color> color to <color=#DF2727>red</color>";
        codeObjects[1].objective = $"Set <color=#EAD565>{codeObjects[1].name}</color> to:\n(0, 20, 10) rotation\n and color green";
        codeObjects[2].objective = $"Set <color=#EAD565>{codeObjects[2].name}</color> to follow the player";
        codeObjects[3].objective = $"Set <color=#EAD565>{codeObjects[3].name}</color> rotation to (0, 0, 75)";
        codeObjects[4].objective = $"Set <color=#EAD565>{codeObjects[4].name}</color> color to <color=#5582F5>blue</color>";
        codeObjects[5].objective = $"Set <color=#EAD565>{codeObjects[5].name}</color> rotation to (14, 0, 12)";
        codeObjects[6].objective = $"Set <color=#EAD565>{codeObjects[6].name}</color> color to <color=#F0DB90>yellow</color>";
        codeObjects[7].objective = $"<color=#EAD565>{codeObjects[7].name}</color> is a blank object to try your code";

        codeObjects[0].correctColor = Color.red;

        codeObjects[1].correctColor = Color.green;
        codeObjects[1].correctX = 0;
        codeObjects[1].correctY = 20;
        codeObjects[1].correctZ = 10;

        codeObjects[3].correctColor = codeObjects[3]._gameObject.GetComponent<Renderer>().material.color;
        codeObjects[3].correctX = 0;
        codeObjects[3].correctY = 0;
        codeObjects[3].correctZ = 75;

        codeObjects[4].correctColor = Color.blue;

        codeObjects[5].correctColor = codeObjects[5]._gameObject.GetComponent<Renderer>().material.color;
        codeObjects[5].correctX = 14;
        codeObjects[5].correctY = 0;
        codeObjects[5].correctZ = 12;

        codeObjects[6].correctColor = Color.yellow;

        codeObjects[7].correctColor = Color.gray;
        codeObjects[7].correctX = 1;
        codeObjects[7].correctY = 1;
        codeObjects[7].correctZ = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F1) && typing)
        {
            typing = false;
            Actions.OnCodeClose();
        }
        if (!typing) UIupdate();
        if (!typing) InteractRaycast();
        if (!typing) MovePlayer();
        if (Input.GetKey(KeyCode.LeftAlt) && typing) InputCheck(currentObj);
    }

    void UIupdate()
    {
        switch (currentUI)
        {
            case UIstate.pointer:
                itrIcon.enabled = false;
                inCode.DeactivateInputField();
                unlockedAction.enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                objective.text = $"Find an interactive object";
                if (inCode.enabled == false) inCode.text = "";
                if (txtFeedback.enabled == false) txtFeedback.text = "";
                break;

            //pointing at an interactable object that is in range to access code
            case UIstate.objectFound:
                itrIcon.enabled = true;
                if(currentObj != null)
                {
                    itrIcon.text = itrIcon.text = $"e <color=#EAD565>{currentObj.name}</color>";
                    if (currentObj == codeObjects[2] && currentObj.c2 == true)
                    {
                        unlockedAction.enabled = true;
                        unlockedAction.text = $"f to toggle <color=#EAD565>Follow</color>()";
                    }
                    if (currentObj != codeObjects[2])
                    {
                        //Debug.Log($"user written z: {currentObj.x} || actually correct z: {currentObj.correctX}");
                        
                        if (currentObj.c1 == true)
                        {
                            unlockedAction.enabled = true;
                            unlockedAction.text = $"challenge completed";
                        }
                        else if(currentObj.incorrect == true)
                        {
                            unlockedAction.enabled = true;
                            unlockedAction.text = $"f to <color=#EAD565>Reset</color>";
                        }
                    }
                }
                progress.enabled = true;
                if (inCode.enabled == false) inCode.text = "";
                if (txtFeedback.enabled == false) txtFeedback.text = "";
                break;

            case UIstate.codeWindow:
                itrIcon.enabled = false;
                progress.enabled = false;
                inCode.Select();
                inCode.ActivateInputField();
                unlockedAction.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;

            default:
                inCode.DeactivateInputField();
                if (inCode.enabled == false) inCode.text = "";
                break;
        }

        inCode.enabled = typing;
        txtFeedback.enabled = typing;
        inCode.image.enabled = typing;
        txtFeedback.image.enabled = typing;
        errorList.enabled = typing;
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void InteractRaycast()
    {
        Vector3 playerPos = cam.transform.position;
        Vector3 fowardDir = cam.transform.forward;

        Ray intrRay = new Ray(playerPos, fowardDir);
        RaycastHit intrRayHit;
        float rayRange = 4.0f;

        bool intrFound = Physics.Raycast(intrRay, out intrRayHit, rayRange);
        if (intrFound)
        {
            GameObject hitObject = intrRayHit.transform.gameObject;
            if (hitObject == ground) currentUI = UIstate.pointer;
            else
            {
                foreach (CodeableObject actObj in codeObjects)
                {
                    if (actObj._gameObject == hitObject)
                    {
                        currentUI = UIstate.objectFound;
                        currentObj = actObj;

                        objective.text = $"Access the code";

                        if (Input.GetKey(KeyCode.E) && typing == false)
                        {
                            currentUI = UIstate.codeWindow;
                            if (actObj.userCode == "") inCode.text = actObj.code;
                            else inCode.text = actObj.userCode;

                            objective.text = $"{currentObj.objective}";
                            objective.text += $"\n\nAlt to <color=#EAD565>Build & Run</color>\n\nF1 to <color=#EAD565>Exit</color>";
                            typing = true;
                            UIupdate();
                            Actions.OnCodeOpen();
                        }
                        else if (Input.GetKey(KeyCode.F) && typing == false)
                        {
                            unlockedAction.text = "";
                            actObj.userCode = "";
                            if (currentObj == codeObjects[2] && currentObj.c2 == true) Actions.OnFollowToggle();
                            if(currentObj != codeObjects[2] && currentObj.c1 == false && currentObj.incorrect == true)
                            {
                                currentObj.incorrect = false;
                                currentObj._gameObject.GetComponent<Renderer>().material.color = Color.white;
                                currentObj._gameObject.transform.Rotate(currentObj.x, currentObj.y, currentObj.z, Space.Self);
                                currentObj._gameObject.transform.SetPositionAndRotation(currentObj.position, currentObj.rotation);
                            }

                        }
                    }
                }
            }
        }
        else currentUI = UIstate.pointer;
    }

    void InputCheck(CodeableObject obj)
    {
        string txt = "", setRotation ="", setColor = "";
        float x = -1, y = -1, z = -1;
        txtFeedback.text = "";
        errorList.text = "";

        obj.userCode = inCode.text;
        txt = inCode.text;
        txt = Regex.Replace(txt, "<.*?>", string.Empty);

        if (!txt.Contains(";"))
        {
            txtFeedback.text = "<color=#EF6446>; expected</color>";
            if (!txt.Contains("{") || !txt.Contains("}"))
            {
                txtFeedback.text += "\n<color=#EF6446>{} expected</color>";
            }
            return;
        }
        

        if (txt.Contains("Rotate("))
        {
            //rotate function called
            string strEnd = txt.Substring(txt.IndexOf("Rotate(") + 7);
            string strPiece = strEnd.Substring(0, strEnd.IndexOf(", S"));
            setRotation = strPiece;

            strPiece = Regex.Replace(strPiece, "f", " ");
            strPiece = Regex.Replace(strPiece, ",", " ");
            
            string[] numbers = Regex.Split(strPiece, @"\D+");
            if (numbers[2] != null)
            {
                x = float.Parse(numbers[0]);
                y = float.Parse(numbers[1]);
                z = float.Parse(numbers[2]);
            }
            else txtFeedback.text += "function missing parameters";
            if (obj.c1 == false)
            {
                obj._gameObject.transform.Rotate(x, y, z, Space.Self);
                obj.x = x;
                obj.y = y;
                obj.z = z;
            }
        }
        if (txt.Contains("material.color"))
        {
            //colour change function called
            string stringAfterChar1 = txt.Substring(txt.IndexOf(".color") + 8);
            string stringBeforeChar1 = stringAfterChar1.Substring(0, stringAfterChar1.IndexOf(";"));
            setColor = stringBeforeChar1;

            setColor = Regex.Replace(setColor, " ", string.Empty);

            if (obj.c1 == false)
            {
                if (setColor == "Color.red") obj._gameObject.GetComponent<Renderer>().material.color = Color.red;
                if (setColor == "Color.blue") obj._gameObject.GetComponent<Renderer>().material.color = Color.blue;
                if (setColor == "Color.green") obj._gameObject.GetComponent<Renderer>().material.color = Color.green;
                if (setColor == "Color.black") obj._gameObject.GetComponent<Renderer>().material.color = Color.black;
                if (setColor == "Color.cyan") obj._gameObject.GetComponent<Renderer>().material.color = Color.cyan;
                if (setColor == "Color.gray") obj._gameObject.GetComponent<Renderer>().material.color = Color.gray;
                if (setColor == "Color.grey") obj._gameObject.GetComponent<Renderer>().material.color = Color.grey;
                if (setColor == "Color.magenta") obj._gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                if (setColor == "Color.white") obj._gameObject.GetComponent<Renderer>().material.color = Color.white;
                if (setColor == "Color.yellow") obj._gameObject.GetComponent<Renderer>().material.color = Color.yellow;

                if (setColor == "Color.new") obj._gameObject.GetComponent<Renderer>().material.color = new Color(0.9f, 0.7f, 0.1f, 1);
            }
        }

        if (obj == codeObjects[2] && obj.c1 == false)
        {
            if (inCode.text.Contains("transform.<color=#F0DB90>LookAt</color>()")) txtFeedback.text += "<color=#EF6446>LookAt() function expects a variable parameter of type Transform</color>";
            else if (inCode.text.Contains("transform.<color=#F0DB90>LookAt</color>(player);"))
            {
                Actions.OnLookAtCorrect();
                if (inCode.text != "") codeObjects[2].code = inCode.text;

                obj.userCode = "";
                codeObjects[2].code = "<color=#5C83CD>public class</color><color=#49B494> Drone</color> : <color=#49B494>MonoBehaviour</color>\n{\n\t\t<color=#5C83CD>public </color><color=#49B494>Transform</color> player;\n\t\t<color=#5C83CD>int</color> speed;\n\t\t<color=#7FB26D>Vector3 </color>pos;\n\n\t\t<color=#5C83CD>public void </color><color=#F0DB90>Follow</color>( )\n\t\t{\n\t\t\t\tpos = <color=#7FB26D>Vector3</color>.<color=#F0DB90>MoveTowards</color>(transform.position, player.position, );\n\t\t\t\tr.<color=#F0DB90>MovePosition</color>(pos);\n\t\t\t\ttransform.<color=#F0DB90>LookAt</color>(player);\n\t\t}\n}";
                
                codeObjects[2].c1 = true;
                inCode.text = "";
                txtFeedback.text = "";
                typing = false;
                Actions.OnCodeClose();
            }
            else
            {
                txtFeedback.text = "<color=#EF6446>unknown error</color>";
            }
        }
        else if (obj == codeObjects[2] && obj.c2 == false)
        {
            if (!inCode.text.Contains(";")) txtFeedback.text = "<color=#EF6446>; expected</color>";
            else if (!inCode.text.Contains("{") && !inCode.text.Contains("}")) txtFeedback.text = "<color=#EF6446>{} expected</color>";
            else if (!inCode.text.Contains("(transform.position, player.position, speed)") && !inCode.text.Contains("speed)"))
            {
                txtFeedback.text = "\n<color=#EF6446>variable 'speed' never used</color>";
                txtFeedback.text += "\n<color=#EF6446>MoveTowards() function requires 3 parameters</color>";
            }
            else if (inCode.text.Contains("transform.position, player.position, speed"))
            {
                codeObjects[2].c2 = true;
                Actions.OnFollowCorrect();
                if (inCode.text != "") codeObjects[2].code = inCode.text;
                inCode.text = "";
                txtFeedback.text = "";
                typing = false;
                Actions.OnCodeClose();
            }
            else
            {
                txtFeedback.text = "<color=#EF6446>unknown error</color>";
            }

        }

        if (txtFeedback.text != "") errorList.text = "<color=#969696>Error List</color>";

        if (obj != codeObjects[2] && obj.c1 == false)
        {
            if(obj.x == obj.correctX && obj.y == obj.correctY && obj.z == obj.correctZ && obj._gameObject.GetComponent<Renderer>().material.color == obj.correctColor)
            {
                obj.c1 = true;
            }
            else obj.incorrect = true;

            inCode.text = "";
            txtFeedback.text = "";
            typing = false;
            Actions.OnCodeClose();
        }
    }

}