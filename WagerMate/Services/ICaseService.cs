using WagerMate.Data;

namespace WagerMate.Services;

public interface ICaseService
{
    public Case CreateCase(Case createdCase);
    
    public Case GetCaseById(int caseId);
    public List<Case> GetCasesByBetId(int betId);
    
    public bool UpdateCase(Case newCase);
    
    public bool DeleteCase(int caseId);
}