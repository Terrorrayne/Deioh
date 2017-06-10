public interface IButton
{
	void ButtonPress(PlayerButtonInteraction player);
	bool ButtonIsActive(); // some buttons will only ever return true
}
