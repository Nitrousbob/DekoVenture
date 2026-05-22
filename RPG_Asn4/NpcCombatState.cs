namespace RPG_Asn4
{
    public class NpcCombatState : IState
    {
    
    public string Name => "Combat";
    private Npc _npc;

    public NpcCombatState(Npc npc)
    {
        _npc = npc;
    }

    public void Enter()
        {
            UI.Narrate($"{_npc.Name} readies for battle!");
        }

    public void Update()
        {
            //Combat Loop AI Logic
            if (_npc.CurrentPlayer == null || !_npc.IsAlive)
            {
                _npc.StateMachine.ChangeState(_npc.IdleState);
                return;
            }
            
            int damage = _npc.EquippedWeapon != null ? _npc.EquippedWeapon.GetDamage():Random.Shared.Next(1,4);
            
            //Apply damage to _npc.CurrentPlayer, armor adjustments
            UI.ShowNpcAction($"{_npc.Name} attacks {_npc.CurrentPlayer.Name} for {damage} damage!");
            _npc.CurrentPlayer.TakeDamage(damage);
            
            //if player or npc dies, statemachine.chagestate(IdleState/DeathState). depending on the duel to the death or just a barfight or something
            if (!_npc.CurrentPlayer.IsAlive)
            {
                _npc.CurrentPlayer = null;
                _npc.StateMachine.ChangeState(_npc.IdleState);
            }
        }

    public void Exit()
        {
            //Combat cleanup, resest aggro, give xp etc.
        }
    
    }

}