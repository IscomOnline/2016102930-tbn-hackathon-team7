using UnityEngine;
using UnityEditor;

public class UnityFile: UnityEditor.EditorWindow
{
	public UnityFile ()
	{
	}

	[UnityEditor.MenuItem( "Example/Overwrite Texture" )]
	public  static Texture2D openImage()
	{
		Texture2D texture = new Texture2D(100,100);
		//if (texture == null) {
			//EditorUtility.DisplayDialog ("Select Texture", "You must select a texture first!", "OK");
			//return ;
		//} 
		string path = UnityEditor.EditorUtility.OpenFilePanel( "Overwrite with png", "", "png" );
		if( path.Length != 0 )
		{
			WWW www = new WWW( "file:///" + path );
			//texture = Selection.activeObject as Texture2D;
			www.LoadImageIntoTexture( texture );
		}
		return texture;
	}
}



