using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public abstract class Piece
{
    public int x;
    public int y;

    public int maxHealth;
    public int hitPoints;
    public int damage;

    public bool isSelected;

    public bool active;

    public enum Type
    {
        AI,
        Human
    }
    public Type type;

    public MonoBehaviour monoBehaviour;

    public Piece(MonoBehaviour mb)
    {
        monoBehaviour = mb;
        isSelected = false;
        active = true;
    }

    public virtual void Select()
    {
        if (!isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, 0.2f, 0.0f));
        isSelected = true;
    }
    public virtual void Unselect()
    {
        if (isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, -0.2f, 0.0f));
        isSelected = false;
    }

    public void Move(int positionX, int positionY, bool animated = true)
    {   
        Game.instance.pieces[x, y] = null;
        Game.instance.pieces[positionX, positionY] = this;
        x = positionX;
        y = positionY;

        Vector3 newPosition = new Vector3(positionX - 3.5f, 0.0f, positionY - 3.5f);
        if (animated)
            MovementAnimation.movementAnimation.AnimateMove(this, monoBehaviour.transform.position, newPosition);
        else
            monoBehaviour.transform.position = newPosition;
    }

    public abstract void Destroy();
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        float health = (float) hitPoints / maxHealth;
        (monoBehaviour as HealthSetter).SetHealth(health);
        if (hitPoints <= 0)
            Destroy();
    }

    public abstract List<Tile> GetPosibleMoves();
    public abstract List<Piece> GetAttackPossibilities(out bool requireChoice);

}

public abstract class AIPiece : Piece
{
    public static Material target = null;
    public static Material AIMaterial = null;

    public AIPiece(MonoBehaviour mb) : base(mb)
    {
        type = Type.AI;
    }

    public override void Select()
    {
        if (target == null)
            target = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Target.mat");

        monoBehaviour.transform.GetChild(1).GetComponent<Renderer>().material = target;
        isSelected = true;
    }

    public override void Unselect()
    {
        if (AIMaterial == null)
            AIMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/AIPiece.mat");

        monoBehaviour.transform.GetChild(1).GetComponent<Renderer>().material = AIMaterial;
        isSelected = false;
    }
}

public abstract class HumanPiece : Piece
{
    public static HashSet<HumanPiece> HumanPieces = new HashSet<HumanPiece>();

    public static Material ordinary = null;
    public static Material highlighted = null;
    
    public HumanPiece(MonoBehaviour mb) : base(mb)
    {
        type = Type.Human;
        HumanPieces.Add(this);
    }

    public override void Destroy()
    {
        Game.instance.pieces[x, y] = null;
        MonoBehaviour.Destroy(monoBehaviour.gameObject);
        HumanPieces.Remove(this);

        if (HumanPieces.Count == 0)
        {
            Game.instance.EndGame(Game.Winner.AI);
        }
    }

    public static Dictionary<HumanPiece, List<Tile>> ActivePieces()
    {
        Dictionary<HumanPiece, List<Tile>> res = new Dictionary<HumanPiece, List<Tile>>();
        foreach (HumanPiece hp in HumanPieces)
        {
            if (hp.active)
            {
                List<Tile> possibleMoves = hp.GetPosibleMoves();
                if (possibleMoves.Count > 0)
                    res.Add(hp, possibleMoves);
            }
        }
        return res;
    }

    public override void Select()
    {
        if (highlighted == null)
            highlighted = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/HumanPieceHighlighted.mat");

        monoBehaviour.transform.GetChild(1).GetComponent<Renderer>().material = highlighted;
        isSelected = true;
    }

    public override void Unselect()
    {
        if (ordinary == null)
            ordinary = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/HumanPiece.mat");

        monoBehaviour.transform.GetChild(1).GetComponent<Renderer>().material = ordinary;
        isSelected = false;
    }
}