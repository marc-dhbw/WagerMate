using WagerMate.Data;


namespace WagerMate.Services.impl;

public class CaseService : ICaseService
{
    private IDbService _service;

    public CaseService(IDbService service)
    {
        _service = service;
    }
    
    public Case CreateCase(Case createdCase)
    {
        _service.Create<Case>("INSERT INTO public.case(bet_id, casetype) VALUES(@Bet_Id, @Casetype)", createdCase);
        return createdCase;
    }

    public Case GetCaseById(int caseId)
    {
        var result = _service.GetById<Case>("SELECT * FROM public.case WHERE Id = @Id", new { id = caseId });
        return result;
    }

    public List<Case> GetCasesByBetId(int betId)
    {
        var result = _service.GetAllWithParams<Case>("SELECT * FROM public.case WHERE bet_id = @Id",new{Id = betId});
        return result;
    }

    public bool UpdateCase(Case newCase)
    {
        var result =
            _service.Update<Case>(
                "UPDATE public.case SET Id=@Id, bet_id=@bet_Id, casetype = @Casetype WHERE Id = @Id", newCase);
        return result;
    }

    public bool DeleteCase(int caseId)
    {
        var result  = _service.Delete<Case>("DELETE FROM public.case WHERE Id = @caseId", new { caseId = caseId });
        return result;
    }

    public bool DeleteCasesOfBetId(int betId)
    {
        var result = _service.Delete<Case>("DELETE FROM public.case where bet_id = @id", new { Id = betId });
        return result;
    }
}