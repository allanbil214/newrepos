public interface IEmployee
{
    void Work();
    void GetPaid();
}

public interface IManager : IEmployee
{
    void ManageTeam();
    void ConductMeetings();
}

public interface ILead : IManager
{
    void ReviewCode();
}

public interface IWorker : IEmployee
{
    void WriteCode();
}