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

    public void Select()
    {
        if (!isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, 0.2f, 0.0f));
        isSelected = true;
    }
    public void Unselect()
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
        if (hitPoints <= 0)
            Destroy();
    }

    public abstract List<Tile> GetPosibleMoves();
    public abstract List<Piece> GetAttackPossibilities(out bool requireChoice);

}

public abstract class AIPiece : Piece
{
    public AIPiece(MonoBehaviour mb) : base(mb)
    {
        type = Type.AI;
    }
}

public abstract class HumanPiece : Piece
{
    public static HashSet<HumanPiece> HumanPieces = new HashSet<HumanPiece>();
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
}