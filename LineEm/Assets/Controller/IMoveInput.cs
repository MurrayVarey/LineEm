using System;


public interface IMoveInput
{
	bool UsesMouseInput();
	void MakeMove();
}

public class PlayerInput : IMoveInput
{
	public bool UsesMouseInput() { return true; }
	public void MakeMove() {}
}

public class CPUInput : IMoveInput
{
	public bool UsesMouseInput() { return false; }
	public void MakeMove() {}
}


