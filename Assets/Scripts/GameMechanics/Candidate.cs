using Photon.Realtime;
using System.Collections.Generic;

public class Candidate
{
    public int candidateNumber = -1;
    public int localPlayerNumber = -1;
    public Player player = null;    
    public List<Cards> cardsInHand= new List<Cards>();
    
}
