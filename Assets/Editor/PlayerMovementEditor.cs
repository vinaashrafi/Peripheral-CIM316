using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


[CustomEditor(typeof(FPController)), InitializeOnLoadAttribute]
public class PlayerMovementEditor : Editor
{
    FPController fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (FPController)target;
        SerFPC = new SerializedObject(fpc);
    }

    public override void OnInspectorGUI()
    {
        SerFPC.Update();


        EditorGUILayout.Space();

        #region Camera Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Camera Setup",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.playerCamera = (Camera)EditorGUILayout.ObjectField(
            new GUIContent("Camera", "Camera attached to the controller."), fpc.playerCamera, typeof(Camera), true);
        fpc.fov = EditorGUILayout.Slider(
            new GUIContent("Field of View", "The camera’s view angle. Changes the player camera directly."), fpc.fov,
            fpc.zoomFOV, 179f);
        fpc.cameraCanMove = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Camera Rotation", "Determines if the camera is allowed to move."),
            fpc.cameraCanMove);

        GUI.enabled = fpc.cameraCanMove;
        fpc.invertCamera = EditorGUILayout.ToggleLeft(
            new GUIContent("Invert Camera Rotation", "Inverts the up and down movement of the camera."),
            fpc.invertCamera);
        fpc.mouseSensitivity =
            EditorGUILayout.Slider(
                new GUIContent("Look Sensitivity", "Determines how sensitive the mouse movement is."),
                fpc.mouseSensitivity, .1f, 10f);
        fpc.maxLookAngle =
            EditorGUILayout.Slider(
                new GUIContent("Max Look Angle", "Determines the max and min angle the player camera is able to look."),
                fpc.maxLookAngle, 40, 90);
        GUI.enabled = true;

        fpc.lockCursor = EditorGUILayout.ToggleLeft(
            new GUIContent("Lock and Hide Cursor",
                "Turns off the cursor visibility and locks it to the middle of the screen."), fpc.lockCursor);

        fpc.crosshair = EditorGUILayout.ToggleLeft(
            new GUIContent("Auto Crosshair",
                "Determines if the basic crosshair will be turned on, and sets is to the center of the screen."),
            fpc.crosshair);

        // Only displays crosshair options if crosshair is enabled
        if (fpc.crosshair)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Crosshair Image", "Sprite to use as the crosshair."));
            fpc.crosshairImage = (Sprite)EditorGUILayout.ObjectField(fpc.crosshairImage, typeof(Sprite), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fpc.crosshairColor = EditorGUILayout.ColorField(
                new GUIContent("Crosshair Color", "Determines the color of the crosshair."), fpc.crosshairColor);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        #region Camera Zoom Setup

        GUILayout.Label("Zoom",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.enableZoom = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Zoom", "Determines if the player is able to zoom in while playing."),
            fpc.enableZoom);

        GUI.enabled = fpc.enableZoom;
        fpc.holdToZoom = EditorGUILayout.ToggleLeft(
            new GUIContent("Hold to Zoom",
                "Requires the player to hold the zoom key instead if pressing to zoom and unzoom."), fpc.holdToZoom);
        fpc.zoomKey =
            (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Zoom Key", "Determines what key is used to zoom."),
                fpc.zoomKey);
        fpc.zoomFOV =
            EditorGUILayout.Slider(new GUIContent("Zoom FOV", "Determines the field of view the camera zooms to."),
                fpc.zoomFOV, .1f, fpc.fov);
        fpc.zoomStepTime =
            EditorGUILayout.Slider(
                new GUIContent("Step Time", "Determines how fast the FOV transitions while zooming in."),
                fpc.zoomStepTime, .1f, 10f);
        GUI.enabled = true;

        #endregion

        #endregion

        #region Movement Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Movement Setup",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.playerCanMove = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Player Movement", "Determines if the player is allowed to move."),
            fpc.playerCanMove);

        GUI.enabled = fpc.playerCanMove;
        fpc.walkSpeed =
            EditorGUILayout.Slider(
                new GUIContent("Walk Speed", "Determines how fast the player will move while walking."), fpc.walkSpeed,
                .1f, fpc.sprintSpeed);
        GUI.enabled = true;

        EditorGUILayout.Space();

        #region Sprint

        GUILayout.Label("Sprint",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.enableSprint = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Sprint", "Determines if the player is allowed to sprint."), fpc.enableSprint);

        GUI.enabled = fpc.enableSprint;
        fpc.unlimitedSprint = EditorGUILayout.ToggleLeft(
            new GUIContent("Unlimited Sprint",
                "Determines if 'Sprint Duration' is enabled. Turning this on will allow for unlimited sprint."),
            fpc.unlimitedSprint);
        fpc.sprintKey =
            (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Sprint Key", "Determines what key is used to sprint."),
                fpc.sprintKey);
        fpc.sprintSpeed =
            EditorGUILayout.Slider(
                new GUIContent("Sprint Speed", "Determines how fast the player will move while sprinting."),
                fpc.sprintSpeed, fpc.walkSpeed, 20f);

        //GUI.enabled = !fpc.unlimitedSprint;
        fpc.sprintDuration =
            EditorGUILayout.Slider(
                new GUIContent("Sprint Duration",
                    "Determines how long the player can sprint while unlimited sprint is disabled."),
                fpc.sprintDuration, 1f, 20f);
        fpc.sprintCooldown =
            EditorGUILayout.Slider(
                new GUIContent("Sprint Cooldown",
                    "Determines how long the recovery time is when the player runs out of sprint."), fpc.sprintCooldown,
                .1f, fpc.sprintDuration);
        //GUI.enabled = true;

        fpc.sprintFOV =
            EditorGUILayout.Slider(
                new GUIContent("Sprint FOV", "Determines the field of view the camera changes to while sprinting."),
                fpc.sprintFOV, fpc.fov, 179f);
        fpc.sprintFOVStepTime =
            EditorGUILayout.Slider(
                new GUIContent("Step Time", "Determines how fast the FOV transitions while sprinting."),
                fpc.sprintFOVStepTime, .1f, 20f);

        fpc.useSprintBar = EditorGUILayout.ToggleLeft(
            new GUIContent("Use Sprint Bar", "Determines if the default sprint bar will appear on screen."),
            fpc.useSprintBar);

        // Only displays sprint bar options if sprint bar is enabled
        if (fpc.useSprintBar)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            fpc.hideBarWhenFull = EditorGUILayout.ToggleLeft(
                new GUIContent("Hide Full Bar",
                    "Hides the sprint bar when sprint duration is full, and fades the bar in when sprinting. Disabling this will leave the bar on screen at all times when the sprint bar is enabled."),
                fpc.hideBarWhenFull);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Bar BG", "Object to be used as sprint bar background."));
            fpc.sprintBarBG = (Image)EditorGUILayout.ObjectField(fpc.sprintBarBG, typeof(Image), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(new GUIContent("Bar", "Object to be used as sprint bar foreground."));
            fpc.sprintBar = (Image)EditorGUILayout.ObjectField(fpc.sprintBar, typeof(Image), true);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            fpc.sprintBarWidthPercent = EditorGUILayout.Slider(
                new GUIContent("Bar Width", "Determines the width of the sprint bar."), fpc.sprintBarWidthPercent, .1f,
                .5f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fpc.sprintBarHeightPercent = EditorGUILayout.Slider(
                new GUIContent("Bar Height", "Determines the height of the sprint bar."), fpc.sprintBarHeightPercent,
                .001f, .025f);
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

        #region Pickup

        GUILayout.Label("Pickup",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.pickupKey =
            (KeyCode)EditorGUILayout.EnumPopup(
                new GUIContent("Pickup Key", "Determines what key is used to pick up objects."),
                fpc.pickupKey);

        fpc.pickupRange =
            EditorGUILayout.Slider(new GUIContent("Pickup Range", "How far the player can reach to pick up objects."),
                fpc.pickupRange, 0.5f, 5f);

        fpc.playerHandTransform =
            (Transform)EditorGUILayout.ObjectField(
                new GUIContent("Player Hand Transform", "The transform where picked-up items will be held."),
                fpc.playerHandTransform, typeof(Transform), true);

        EditorGUILayout.Space();

        #endregion
        
        #region Inspect

        GUILayout.Label("Inspect",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.inspectKey = 
            (KeyCode)EditorGUILayout.EnumPopup(
                new GUIContent("Inspect Key", "Determines what key is used to enter/exit inspect mode."),
                fpc.inspectKey);

        #endregion

        #region Drop

        GUILayout.Label("Drop",
            new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Bold,
                fontSize = 13
            },
            GUILayout.ExpandWidth(true));

// Drop Key
        fpc.dropKey = (KeyCode)EditorGUILayout.EnumPopup(
            new GUIContent("Drop Key", "Determines what key is used to drop the currently held item."),
            fpc.dropKey);

// Show current held item (read-only)
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField(
            new GUIContent("Held Item", "The item currently held by the player (read-only)."),
            fpc.heldItem, typeof(GameObject), true);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        #endregion

        #region Chores

        GUILayout.Label("Chores",
            new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontStyle = FontStyle.Bold,
                fontSize = 13
            },
            GUILayout.ExpandWidth(true));

        // Toggle to enable/disable Chore interaction
        fpc.enableChores = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Chores", "Determines if the player can interact with chores."),
            fpc.enableChores
        );

        GUI.enabled = fpc.enableChores;

        // Toggle for Hold To Complete or Tap To Complete
        fpc.holdToCompleteChore = EditorGUILayout.ToggleLeft(
            new GUIContent("Hold To Complete Chores",
                "If enabled, the player must hold the interaction key to complete a chore. Otherwise, just tapping will work."),
            fpc.holdToCompleteChore
        );

        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

        #region Jump

        GUILayout.Label("Jump",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.enableJump =
            EditorGUILayout.ToggleLeft(new GUIContent("Enable Jump", "Determines if the player is allowed to jump."),
                fpc.enableJump);

        GUI.enabled = fpc.enableJump;
        fpc.jumpKey =
            (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Jump Key", "Determines what key is used to jump."),
                fpc.jumpKey);
        fpc.jumpPower =
            EditorGUILayout.Slider(new GUIContent("Jump Power", "Determines how high the player will jump."),
                fpc.jumpPower, .1f, 20f);
        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

        #region Crouch

        GUILayout.Label("Crouch",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));

        fpc.enableCrouch = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Crouch", "Determines if the player is allowed to crouch."), fpc.enableCrouch);

        GUI.enabled = fpc.enableCrouch;
        fpc.holdToCrouch = EditorGUILayout.ToggleLeft(
            new GUIContent("Hold To Crouch",
                "Requires the player to hold the crouch key instead if pressing to crouch and uncrouch."),
            fpc.holdToCrouch);
        fpc.crouchKey =
            (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Crouch Key", "Determines what key is used to crouch."),
                fpc.crouchKey);
        fpc.crouchHeight =
            EditorGUILayout.Slider(
                new GUIContent("Crouch Height", "Determines the y scale of the player object when crouched."),
                fpc.crouchHeight, .1f, 1);
        fpc.speedReduction =
            EditorGUILayout.Slider(
                new GUIContent("Speed Reduction",
                    "Determines the percent 'Walk Speed' is reduced by. 1 being no reduction, and .5 being half."),
                fpc.speedReduction, .1f, 1);
        GUI.enabled = true;

        #endregion

        #endregion

        #region Head Bob

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Head Bob Setup",
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.enableHeadBob = EditorGUILayout.ToggleLeft(
            new GUIContent("Enable Head Bob", "Determines if the camera will bob while the player is walking."),
            fpc.enableHeadBob);


        GUI.enabled = fpc.enableHeadBob;
        fpc.joint = (Transform)EditorGUILayout.ObjectField(
            new GUIContent("Camera Joint", "Joint object position is moved while head bob is active."), fpc.joint,
            typeof(Transform), true);
        fpc.bobSpeed =
            EditorGUILayout.Slider(new GUIContent("Speed", "Determines how often a bob rotation is completed."),
                fpc.bobSpeed, 1, 20);
        fpc.bobAmount = EditorGUILayout.Vector3Field(
            new GUIContent("Bob Amount", "Determines the amount the joint moves in both directions on every axes."),
            fpc.bobAmount);
        GUI.enabled = true;

        #endregion
        
        #region Inspect Settings

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Inspect Settings",
            
            
            new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 },
            GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        
        fpc.inspectDistance = EditorGUILayout.Slider(
            new GUIContent("Inspect Distance", "Distance in front of the inspect camera where the object will be placed."),
            fpc.inspectDistance, 0.5f, 5f);
        
        fpc.inspectCamera = (Camera)EditorGUILayout.ObjectField(
            new GUIContent("Inspect Camera", "Camera used when inspecting objects."), 
            fpc.inspectCamera, typeof(Camera), true);

        fpc.inspectRotationSpeed = EditorGUILayout.FloatField(
            new GUIContent("Rotation Speed", "Speed at which inspected object rotates."), 
            fpc.inspectRotationSpeed);

        // Add the inspect panel object field here:
        fpc.inspectPanel = (GameObject)EditorGUILayout.ObjectField(
            new GUIContent("Inspect Panel", "UI panel that appears during inspection (optional)."),
            fpc.inspectPanel, typeof(GameObject), true);

        EditorGUILayout.Space();

        #endregion

        //Sets any changes from the prefab
        if (GUI.changed)
        {
            EditorUtility.SetDirty(fpc);
            Undo.RecordObject(fpc, "FPC Change");
            SerFPC.ApplyModifiedProperties();
        }
    }
}