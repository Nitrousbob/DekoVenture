namespace DekoVenture
{
    public class NpcIdleState : IState
    {
        private readonly Npc npc;
        public string Name => "Idle";

        public NpcIdleState(Npc npc)
        {
            this.npc = npc;
        }

        public void Enter() { }
        public void Update()
        {
            int chance = Random.Shared.Next(100);
            if (chance < 15) // 15% chance to pull out the yoyo or other
            {
                npc.StateMachine.ChangeState(npc.BusyState);
            }
            else if (chance < 90) // 75% chance to do an idle action
            {
                UI.ShowNpcAction($"<LGr>{npc.Name}</LGr> {npc.IdleAction}.");
            }
        }
        public void Exit() { }
    }

    public class NpcBusyState : IState
    {
        private readonly Npc npc;
        public string Name => "Busy";

        public NpcBusyState(Npc npc)
        {
            this.npc = npc;
        }

        public void Enter()
        {
            UI.ShowNpcAction($"<LGr>{npc.Name}</LGr> {npc.BusyStart}");
        }
        public void Update()
        {
            if (Random.Shared.Next(100) < 50) // 50% chance to put it away
            {
                UI.ShowNpcAction($"<LGr>{npc.Name}</LGr> {npc.BusyEnd}.");
                npc.StateMachine.ChangeState(npc.IdleState);
            }
            else
            {
                UI.ShowNpcAction($"<LGr>{npc.Name}</LGr> {npc.BusyAction}");
            }
        }
        public void Exit() { }
    }

    public class NpcTalkingState : IState
    {
        private readonly Npc npc;
        public string Name => "Talking";

        public NpcTalkingState(Npc npc)
        {
            this.npc = npc;
        }

        public void Enter()
        {
            string greeting = DialogFactory.GetRandomGreeting(npc);
            UI.NpcTalkText($"<LGr>{npc.Name}</LGr> says: '{greeting}'");
        }

        public void Update()
        {
            if (npc.CurrentPlayer == null)
            {
                npc.StateMachine.ChangeState(npc.IdleState);
                return;
            }

            bool isTalking = true;
            while (isTalking)
            {
                isTalking = DialogFactory.HandleDialogTurn(npc, npc.CurrentPlayer);
            }

            npc.CurrentPlayer = null; // Clear the player reference
            npc.StateMachine.ChangeState(npc.IdleState); // Fall back to idle
        }

        public void Exit() { }
    }
}